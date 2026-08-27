---
name: nuget-update
description: >-
  Проверить и обновить версии NuGet-пакетов в активных шаблонах NET10.0
  (Module, IdentityModule, RazorPages). Используй, когда пользователь просит
  "проверь обновления пакетов", "обнови nuget", "подними версии зависимостей"
  в текущей папке шаблонов. По умолчанию применяет patch + minor, major —
  только с подтверждения. В конце — сборка трёх решений и (опционально)
  сквозной тест авторизации Module ↔ IdentityModule.
---

# Обновление NuGet-пакетов в шаблонах NET10.0

## Область

- Работаем **только** с `NET10.0/` (активная версия платформы). `NET8.0/` и `NET9.0/` — заморожены, не трогаем.
- Три шаблона, редактируем `.csproj` внутри `content/`:
  - `Calabonga.Microservice.Module.Template` — `*.Web`, `*.Infrastructure`
  - `Calabonga.Microservice.IdentityModule.Template` — `*.Web`, `*.Infrastructure`
  - `Calabonga.AspNetCoreRazorPages.Template` — `*.Web`, `*.Infrastructure`
- **Никогда** не редактируем `*.Domain.csproj` (в них и так нет `PackageReference` — см. `rules/architecture.md`). Если после правок `git status` показывает изменения в `*.Domain` — откатить их (`git checkout --`).

## Шаг 1. Ветка

Создать ветку до любых правок: `chore/nuget-updates-net10` (или с уточняющим суффиксом). Префикс — `chore/` (разрешённые: `feature/ bugfix/ hotfix/ chore/ docs/`). Тип коммита в конце — `build:`.

## Шаг 2. Собрать список доступных обновлений

Запустить вспомогательный скрипт (обходит все `content/**/*.csproj`, тянет последние stable-версии с nuget.org, классифицирует bump):

```bash
pwsh -File .claude/skills/nuget-update/scripts/check-nuget-updates.ps1
```

Скрипт печатает таблицу: `Package | Current | Latest | Bump (major/minor/patch) | Projects`.

## Шаг 3. Согласовать объём

- **patch + minor** — применяем по умолчанию, без вопросов.
- **major** — по каждому major-скачку кратко описать риск (breaking changes, смена TFM) и спросить подтверждение отдельно. Прецедент: `Calabonga.AspNetCore.AppDefinitions` 4.0.0 → 10.0.0 оказался безопасным (номер версии просто трекает версию .NET; из изменений — перевод на net10.0 и миграция `.sln` → `.slnx`, без изменений API).

## Шаг 4. Применить

- Правим `.csproj` инструментом **Edit** (он сохраняет CRLF). **Не** использовать `sed -i` в Git Bash — он перезапишет файл в LF и даст ложный diff на весь файл. Если нужен пакетный replace из шелла — `perl -i -pe 's/\bVersion="X"/Version="Y"/'` (сохраняет CRLF).
- **Согласованность:** одна и та же версия одного пакета во всех шаблонах, где он встречается (`rules` → "Изменение общей обвязки применяем во всех подходящих шаблонах").
- Сохранять атрибуты вроде `NoWarn="NU1605"` на `PackageReference`, если они были.
- Не трогать комментарии `ATTENTION!`.

## Шаг 5. Сборка

Собрать три решения, убедиться что нет новых warning/error:

```bash
dotnet build NET10.0/Calabonga.Microservice.Module.Template/content/Calabonga.Microservice.Module.slnx
```
```bash
dotnet build NET10.0/Calabonga.Microservice.IdentityModule.Template/content/Calabonga.Microservice.IdentityModule.slnx
```
```bash
dotnet build NET10.0/Calabonga.AspNetCoreRazorPages.Template/content/Calabonga.AspNetCoreRazorPages.slnx
```

Известные предсуществующие (НЕ регрессии): CS8601/CS8620 nullable в `Logout.cshtml.cs`; NU1901 low-severity на транзитивных `NuGet.Packaging`/`NuGet.Protocol` через `Microsoft.VisualStudio.Web.CodeGeneration.Design`.

## Шаг 6. Сквозной тест авторизации (по запросу / для major-обновлений)

Проверяем, что IdentityModule (STS) выдаёт токен, а Module его принимает.

1. Установить шаблоны и сгенерировать проекты. **Имя проекта не должно содержать сегмент `Identity`** — коллизия с базовым типом `Identity<Guid>` из Domain (CS0118). Пример: `DemoAuth.Sts`, `DemoAuth.Api`.
   ```bash
   dotnet new install ./NET10.0/Calabonga.Microservice.IdentityModule.Template
   dotnet new install ./NET10.0/Calabonga.Microservice.Module.Template
   dotnet new microservice-oidd -n DemoAuth.Sts -o ./scratch/DemoAuth.Sts
   dotnet new microservice     -n DemoAuth.Api -o ./scratch/DemoAuth.Api
   ```
2. Запустить оба (Development, Kestrel): STS на `https://localhost:10001`, API на `https://localhost:20001`. Дождаться `LISTENING` через `netstat -ano | grep :10001` / `:20001` (фоновый `dotnet run` может отрапортовать "exit 0", хотя процессы живы).
3. Получить токен по `client_credentials`:
   - endpoint `POST https://localhost:10001/connect/token`
   - `client_id=client-id-sts`, `client_secret=client-secret-sts`, `grant_type=client_credentials`, `scope=api`
   - OpenIddict pretty-print'ит JSON — парсить токен регуляркой `"access_token": *"[^"]+"`.
4. Дёрнуть защищённый эндпоинт API **без** токена → ждём `401` + JSON `{error, error_description}`.
5. Тот же запрос с `Authorization: Bearer <token>` → ждём `200` + seed-данные.
   - URL: `GET https://localhost:20001/api/event-items/paged/0` (именно этот путь защищён; `MapPut` — нет).
6. Токен должен быть RS256 JWT с `iss: https://localhost:10001/`, `client_id: client-id-sts`, `scope: api`.
7. Погасить процессы, удалить `./scratch`, `dotnet new uninstall` оба шаблона.

## Шаг 7. Коммит и PR

- Один атомарный коммит: `build: обновление NuGet-пакетов в шаблонах NET10.0` с таблицей версий в теле.
- Завершать сообщение коммита строкой `Co-Authored-By: Claude Sonnet 5 <noreply@anthropic.com>`.
- PR (по запросу): тело с таблицей версий и чек-листом проверки, завершать `🤖 Generated with [Claude Code](https://claude.com/claude-code)`.
- Перед коммитом проверить `git diff --stat` — только `.csproj` в `content/`, только числа версий, никаких `*.Domain`, никаких EOL-warning.
