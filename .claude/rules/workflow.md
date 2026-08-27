## Правила рабочего процесса

- Всегда создавай отдельную ветку Git перед внесением изменений.
- Допускаются следующие наименования веток: `feature/`, `bugfix/`, `hotfix/`, `chore/`, `docs/`
- Формат коммитов: `type: description`, где `type` — Conventional Commits: `feat, fix, refactor, test, docs, style, perf, build, chore, revert`. Для веток `bugfix/` и `hotfix/` тип коммита — `fix:`.
- Если в решении есть тесты — запускай `dotnet test` после реализации и перед коммитом. Тест-проекта в репозитории пока нет.
- Никогда не изменяй файлы без явного разрешения в папках `*.Domain` (во всех версиях платформы — `NET8.0/`, `NET9.0/`, `NET10.0/` — и в `content/`):
  * `Calabonga.AspNetCoreRazorPages.Domain`
  * `Calabonga.Microservice.IdentityModule.Domain`
  * `Calabonga.Microservice.Module.Domain`
  * Добавление новых доменных контрактов — легитимная причина запросить такое разрешение (см. `architecture.md`).
- Если требуется создать новые классы, проверь на наличие файлов с таким же названием в решении.
- Создавайте атомарные коммиты — одно логическое изменение на коммит.