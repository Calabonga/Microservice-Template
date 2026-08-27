## Правила архитектуры

- Направление зависимостей: `Web` → `Infrastructure` → `Domain`. `Domain` не ссылается ни на один из верхних слоёв.
- `Domain` не имеет внешних зависимостей: `*.Domain.csproj` не содержит ни одного `PackageReference` (только сущности, базовые типы и контракты: `IAuditable`, `IHaveId`, `IViewModel`, `Identity`, `Auditable`, `NamedIdentity`, `AppData`).
- Интерфейсы определяют `Domain` и/или сторонние компоненты; `Infrastructure` и `Web` их реализуют.
  - `Infrastructure` содержит `ApplicationDbContext`, `DatabaseInitializer` и (в IdentityModule) типы ASP.NET Core Identity; ссылается на `Domain` и на EF Core / `Calabonga.UnitOfWork` / `Calabonga.Results`.
  - Инфраструктурные абстракции (`IUnitOfWork`, репозитории) приходят из пакета `Calabonga.UnitOfWork`, а не объявляются в `Domain`.
  - Прикладные сервисы (`IAccountService` и т.п.) объявляются и реализуются в `Web/Application/Services`. Новые **доменные** контракты — в `Domain` (правку папок `*.Domain` см. в `workflow.md`).
