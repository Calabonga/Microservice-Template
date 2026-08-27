## Именование

CQRS-операции в шаблоне оформлены как `public static class` с вложенными типами `Request` и `Handler` (паттерн `Mediator`, не MediatR — на верхнем уровне `Command`/`Handler`-типов нет).

- Класс операции — по HTTP-глаголу + сущность: `Get[Entity]ById`, `Get[Entity]Paged`, `Post[Entity]`, `Put[Entity]`, `Delete[Entity]`.
  - Файл называется по классу. Историческое исключение: файл `UpdateEventItem.cs` содержит класс `PutEventItem`.
- Внутри класса операции:
  - `public record Request(...) : IRequest<Operation<[Entity]ViewModel, string>>;`
  - `public class Handler(...) : IRequestHandler<Request, Operation<[Entity]ViewModel, string>>` — имя ровно `Handler`, конструктор первичный.
- Модели представлений: `[Entity]ViewModel`, `[Entity]CreateViewModel`, `[Entity]UpdateViewModel` (суффикс `ViewModel` в конце имени).
- Валидаторы: `[Entity]Validator` (FluentValidation), файл `[Entity]Validator.cs`.
- Маппинг: `[Entity]Mapping` — `static class` с extension-методами `MapToViewModel()`, `MapTo[Entity]()`, `MapUpdatesFrom()` (рукописно, без AutoMapper).
- Endpoints: `[Entity]Endpoints` (наследник `AppDefinition`) + `internal static class [Entity]EndpointsExtensions` с методом `Map[Entity]Endpoints`.
