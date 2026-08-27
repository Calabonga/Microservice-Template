#Requires -Version 7
<#
.SYNOPSIS
    Сканирует PackageReference во всех content/**/*.csproj активной папки NET10.0
    и сравнивает с последними stable-версиями на nuget.org.
.NOTES
    Только чтение. Ничего не меняет. Запуск:
        pwsh -File .claude/skills/nuget-update/scripts/check-nuget-updates.ps1
#>
[CmdletBinding()]
param(
    # Корень активной версии платформы. По умолчанию — NET10.0 в корне репозитория.
    [string]$Root = (Join-Path $PSScriptRoot '..\..\..\..\NET10.0'),
    [switch]$IncludePrerelease
)

$ErrorActionPreference = 'Stop'
try { [Console]::OutputEncoding = [Text.Encoding]::UTF8 } catch { }

if (-not (Test-Path $Root)) {
    throw "Не найдена папка: $Root"
}
$Root = (Resolve-Path $Root).Path
Write-Host "Скан: $Root" -ForegroundColor Cyan

$csproj = Get-ChildItem -Path $Root -Recurse -Filter *.csproj |
    Where-Object { $_.FullName -match '[\\/]content[\\/]' -and $_.Name -notmatch '\.Domain\.csproj$' }

if (-not $csproj) { throw "Не найдено ни одного content/**/*.csproj" }

$rx = [regex]'<PackageReference\s+Include="(?<id>[^"]+)"\s+Version="(?<ver>[^"]+)"'
$refs = foreach ($f in $csproj) {
    $text = Get-Content -Raw -LiteralPath $f.FullName
    foreach ($m in $rx.Matches($text)) {
        [pscustomobject]@{
            Id      = $m.Groups['id'].Value
            Version = $m.Groups['ver'].Value
            Project = $f.Name
        }
    }
}

function ConvertTo-SortableVersion {
    param([string]$Raw)
    $core = ($Raw -split '[-+]')[0]
    $p = @($core -split '\.') + @('0', '0', '0', '0')
    [version]("{0}.{1}.{2}.{3}" -f [int]$p[0], [int]$p[1], [int]$p[2], [int]$p[3])
}

function Get-LatestVersion {
    param([string]$Id, [bool]$Pre)
    $url = "https://api.nuget.org/v3-flatcontainer/$($Id.ToLowerInvariant())/index.json"
    try {
        $data = Invoke-RestMethod -Uri $url -TimeoutSec 30
    } catch {
        return $null
    }
    $versions = $data.versions
    if (-not $Pre) { $versions = $versions | Where-Object { $_ -notmatch '-' } }
    if (-not $versions) { return $null }
    ($versions | Sort-Object { ConvertTo-SortableVersion $_ } | Select-Object -Last 1)
}

function Get-BumpKind {
    param([string]$From, [string]$To)
    if (-not $To) { return 'unknown' }
    $f = @(($From -split '[-+]')[0] -split '\.') + @('0', '0', '0')
    $t = @(($To   -split '[-+]')[0] -split '\.') + @('0', '0', '0')
    if ([int]$t[0] -ne [int]$f[0]) { return 'major' }
    if ([int]$t[1] -ne [int]$f[1]) { return 'minor' }
    if ([int]$t[2] -ne [int]$f[2]) { return 'patch' }
    return 'none'
}

$results = foreach ($g in ($refs | Group-Object Id | Sort-Object Name)) {
    $current = @($g.Group.Version | Sort-Object -Unique)
    $latest  = Get-LatestVersion -Id $g.Name -Pre:$IncludePrerelease.IsPresent
    [pscustomobject]@{
        Package  = $g.Name
        Current  = ($current -join ', ')
        Latest   = $latest
        Bump     = (Get-BumpKind -From $current[0] -To $latest)
        Projects = (@($g.Group.Project | Sort-Object -Unique) -join '; ')
    }
}

$order = @{ major = 0; minor = 1; patch = 2; none = 3; unknown = 4 }
$results |
    Sort-Object @{ e = { $order[$_.Bump] } }, Package |
    Format-Table -AutoSize -Wrap

$upd = @($results | Where-Object { $_.Bump -in 'major', 'minor', 'patch' })
Write-Host ""
Write-Host ("Обновлений доступно: {0}  (major: {1}, minor: {2}, patch: {3})" -f `
    $upd.Count,
    @($upd | Where-Object Bump -eq 'major').Count,
    @($upd | Where-Object Bump -eq 'minor').Count,
    @($upd | Where-Object Bump -eq 'patch').Count) -ForegroundColor Green
Write-Host "По умолчанию применяются patch + minor. Major — только с подтверждения." -ForegroundColor Yellow
