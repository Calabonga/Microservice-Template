# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Что это за репозиторий

Это **не приложение**, а набор шаблонов проектов `dotnet new` («Nimble Framework»), публикуемых как NuGet-пакеты. Каждый шаблон при установке генерирует полноценное решение микросервиса.

Шаблоны разложены по папкам под конкретную версию платформы. **Актуальна `NET10.0/`** (v10.0.0). `NET8.0/` и `NET9.0/` — замороженные копии старых релизов, оставленные для справки; не изменяйте их без явной просьбы.

`NET10.0/` содержит три шаблона:

| Папка | Короткое имя `dotnet new` | Назначение |
| --- | --- | --- |
| `Calabonga.Microservice.Module.Template` | `microservice` | Web API микросервис без сервера авторизации (проверяет токены, выданные извне) |
| `Calabonga.Microservice.IdentityModule.Template` | `microservice-oidd` | То же плюс **сервер авторизации** OAuth2/OIDC на OpenIddict |
| `Calabonga.AspNetCoreRazorPages.Template` | `microservice-razorpages` | UI на Razor Pages, выступающий как OIDC-клиент |

Каждая папка шаблона содержит:
- проект упаковки (`*.Template.csproj`, `<PackageType>Template</PackageType>`) — именно он пакуется и публикуется;
- `content/` — собственно решение, которое генерируется. `sourceName` равен `Calabonga.Microservice.Module` / `...IdentityModule` / `Calabonga.AspNetCoreRazorPages`; движок шаблонов заменяет его на имя, выбранное пользователем.
- `content/.template.config/template.json` — метаданные и параметры шаблона.

## Часто используемые команды

Работайте с решением внутри `content/` конкретного шаблона (пути даны для шаблона Module; для остальных папок подставьте нужные):

```bash
dotnet build NET10.0/Calabonga.Microservice.Module.Template/content/Calabonga.Microservice.Module.slnx
```

```bash
dotnet run --project NET10.0/Calabonga.Microservice.Module.Template/content/Calabonga.Microservice.Module.Web
```

URL-адреса для разработки (профиль запуска Kestrel, `ASPNETCORE_ENVIRONMENT=Development`, Swagger UI на `/swagger`):
- Module → `https://localhost:20001`
- IdentityModule → `https://localhost:10001`
- RazorPages → `https://localhost:30001`

Упаковка шаблона (то же делает CI):

```bash
dotnet pack NET10.0/Calabonga.Microservice.Module.Template/Calabonga.Microservice.Module.Template.csproj -c Release -o ./artifacts
```

Проверка генерации шаблона целиком:

```bash
dotnet new install ./NET10.0/Calabonga.Microservice.Module.Template
dotnet new microservice -n My.Service -o ./scratch/My.Service
dotnet new uninstall ./NET10.0/Calabonga.Microservice.Module.Template
```

**Автоматических тестов в репозитории нет.** CI (`.github/workflows/*.yml`) запускается только вручную (`workflow_dispatch`) и лишь пакует каждый шаблон и пушит в nuget.org.

## Архитектура генерируемого решения

Три проекта, послойная чистая архитектура (`Web` → `Infrastructure` → `Domain`):

- **`*.Domain`** — сущности и базовые типы (`Identity`, `Auditable`, `NamedIdentity`, `Sortable`, …), а также `AppData` — `static partial class` с константами: имя CORS-политики, имена ролей, `Roles`, строки сообщений `AppData.Exceptions.*`.
- **`*.Infrastructure`** — `ApplicationDbContext`, `DatabaseInitializer.Seed(...)`. В IdentityModule здесь же типы ASP.NET Core Identity (`ApplicationUser`, `ApplicationRole`, `ApplicationUserStore`, `ApplicationUserProfile`) и привязка сущностей OpenIddict.
- **`*.Web`** — хост Minimal API.

### Загрузка через AppDefinitions

`Program.cs` минимален: настройка Serilog, затем `builder.AddDefinitions(typeof(Program))` и `app.UseDefinitions()` (из пакета `Calabonga.AspNetCore.AppDefinitions`). Каждый сквозной аспект — класс-наследник `AppDefinition` с методами `ConfigureServices(WebApplicationBuilder)` и/или `ConfigureApplication(WebApplication)`, обнаруживаемый через reflection. Они лежат в `Web/Definitions/<Concern>/`. **Endpoints тоже являются `AppDefinition`** (`Web/Endpoints/*Endpoints.cs`) — они регистрируют группу маршрутов minimal API и делегируют в `IMediator`.

Чтобы добавить функциональность, как правило, добавляют или правят `AppDefinition`, а не `Program.cs`.

### CQRS на Mediator (не MediatR)

Используется [`Mediator`](https://github.com/martinothamar/Mediator) через `Mediator.SourceGenerator` (генерация исходников, без reflection). MediatR и AutoMapper убраны намеренно.

Пары request/handler группируются по одному на файл в `Web/Application/Messaging/<Feature>Messages/Queries/` в виде:

```csharp
public static class GetEventItemById
{
    public record Request(Guid Id) : IRequest<Operation<EventItemViewModel, string>>;

    public class Handler(IUnitOfWork unitOfWork) : IRequestHandler<Request, Operation<EventItemViewModel, string>>
    {
        public async ValueTask<Operation<EventItemViewModel, string>> Handle(Request request, CancellationToken ct) { ... }
    }
}
```

Соглашения:
- Возвращайте `Calabonga.OperationResults.Operation<TModel, TError>` — `Operation.Result(model)` при успехе, `Operation.Error("...")` при ошибке. Не бросайте исключения для ожидаемых ошибок.
- Доступ к данным через `Calabonga.UnitOfWork` — `unitOfWork.GetRepository<TEntity>()`, затем `unitOfWork.SaveChangesAsync()` и проверка `unitOfWork.Result`.
- Маппинг — рукописные extension-методы в `<Feature>Mapping.cs` (`MapToViewModel()`, `MapToEntity()`, `MapUpdatesFrom()`); каждый явно обрабатывает/возвращает `null`.
- Валидация: валидаторы FluentValidation в `<Feature>Validator.cs`, глобально применяются через `ValidatorBehavior<,>` (это `IPipelineBehavior`), который бросает `ValidationException`.
- Прочие pipeline behaviors: `TransactionBehavior` (оборачивает handler в транзакцию UoW), `LogPostTransaction` / `EventItemPostTransactionBehavior`.

### Хранение данных

`DbContextDefinition` регистрирует `UseInMemoryDatabase("DEMO_PURPOSES_ONLY")` — только для демонстрации. Переход на реальный провайдер (SqlServer и т.п.) — задокументированный шаг для разработчика, см. комментарии `ATTENTION!`. `DataSeedingDefinition` вызывает `DatabaseInitializer.Seed`; миграции (`context.Database.MigrateAsync()`) закомментированы из-за in-memory по умолчанию.

### Дополнения в IdentityModule

- `OpenIddictDefinition` — потоки auth-code + PKCE, client-credentials, password и refresh-token; scopes `email profile roles api custom`; ephemeral/dev-ключи подписи и шифрования (только для разработки).
- `OpenIddictWorker` (`IHostedService`) — `EnsureCreatedAsync` + сидирование двух демо-клиентов: `client-id-sts` (service-to-service) и `client-id-code` (auth-code, с redirect URI для примеров Swagger/Blazor/RazorPages).
- `Pages/Connect/` — UI входа/выхода на Razor Pages; `Endpoints/AuthorizeEndpoints.cs`, `TokenEndpoints.cs`; `Application/Services/AccountService`.
- В in-memory-режиме пользователи пересоздаются при каждом старте — очищайте cookie/данные сайта в браузере между запусками (см. `ATTENTION` в `AuthorizeEndpoints.cs`).

## При редактировании шаблонов

- Ищите комментарии `ATTENTION!` — они отмечают намеренные точки принятия решения, оставленные потребителю шаблона; сохраняйте их осмысленность, не «исправляйте» их.
- Шаблоны Module, IdentityModule и RazorPages делят большую часть инфраструктуры (базовые типы, `AppData`, definitions, pipeline behaviors). Изменение общей обвязки обычно нужно применить во всех подходящих шаблонах в `NET10.0/`, чтобы они оставались согласованными.
- `net10.0`, `Nullable` enable, `ImplicitUsings` enable; часть global usings вынесена в `GlobalUsing.cs` по проектам.
- Решения используют формат `.slnx` (XML). Генерируемое решение — `content/*.slnx`; прочие `.slnx` в корне шаблона или в `.vs/` — служебные файлы IDE.
- Вспомогательные папки: `AuthClientSamples/BlazorWebApp` (пример OIDC-клиента), `Whatnot/` (картинки для README), `README.md` / `README-en.md` (RU/EN, содержат полную историю версий).
