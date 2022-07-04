# Информация
Справочная информация по фреймворку есть в [Wiki](https://github.com/Calabonga/Microservice-Template/wiki) и на [сайте разработчика](https://www.calabonga.net/blog/post/microservice-templates). Видео презентация новой версии [Nimble Framework v.6](https://youtu.be/euOLhhNEcwg).

# Новости

### 2022-07-04

В новой версии 6.1.1 удалены проекты из папки FullAPI (вместе с папкой). Версия с IdentityServer больше не будет получать обновления, но пока останется доступной. Вы можете ее найти в папке MinimalAPI-IS4. Если вам еще нужны эти проекты, настоятельно рекомендую сделать fork моего репозитория.

### 2022-05-14
На платформе NET6 (В папке AspNetCore v6.1) можно найти новую версию Nimble Framework, который предназначен для быстрого создания микросервисной архитектуры. Nimble Framework содержит IdentityModule (AuthServer) и Module (microservice). Новая версия доступна для скачивания. 

[В статье блога](https://www.calabonga.net/blog/post/nimble-framework-v-6-1) и [Видео презентация](https://youtu.be/xijBGwMEL8E)

### 2022-05-11
Реализация OpenIddict в шаблонах завершена, теперь формируются шаблоны для Visual Studio. =

### 2022-05-04
Начались работы по переходу от IdentityServer4 на OpenIddict как основной сервис авторизации OAuth2.0. Причина переезда - скорое окончание срока поддержки IdentityServer4 и перехода его на платную основу.

### 2021-11-21

На платформе NET6 (В папке `AspNetCore v6.0`) теперь существует два вида шаблонов микросервисов (FullAPI и MinimalAPI), каждый из которых содержит IdentityModel и простой Module. Папка FullAPI содержит уже знакомые шаблоны микросервисов, просто они теперь переведены на NET6. А в папке MinimalAPI новая версия тех же шаблонов, которые построены на базе [MinimalAPI](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0). 

### 2021-11-01
1. Удалены старые шаблоны для версий NET Core 2.2 и NET Core 3.0. 
2. Началась разработка шаблонов для новой версии.

# История версий Nimble Framework

v6.1.1 от 2022-07-04

* Обновлены nuget-пакеты, которые использованы в шаблонах (EntityFramework, FluentValidation).
* Удалены старые проекты.
* Обновлены собранные архивы для Visual Studio в папке OUTPUT.

v6.1.0 от 2022-05-14
* Удален IdentityServer4 как основа для аутентификации и авторизации на базе OAuth2.0. Теперь Nimble Framework использует для шаблона с сервером авторизации OpenIddict, который также реализует спецификации OAuth2.0. 
* Обновлен механизм авторизации на странице swagger. Теперь используется Authorization Code Flow, который реализует форму входа. Это позволит использовать шаблон "из коробки".
* Обновлен механизм авторизации для второстепенных сервисов (механизм передачи ролей в токен)
* Обновлены почти все сборки (переведены на NET6), которые использованы в шаблонах
* Реализована система Definitions

v6.0.1 от 2022-01-16
1. Обновлены nuget-пакеты Automapper и Mediatr
2. Обновились сборки для ASP.NET Core, EntityFrameworkCore
3. Добавлена информация к шаблону о его времени сборке и прочая информация про Copyright
4. Обновлен "сборщик" шаблонов, который позволяет быстро обновлять solutions

v6.0.0 от 2021-11-12
1. Шаблоны микросервисов переведены на новую версию NET6. Обновлены почти все файлы, использованы новые "фишки", которые появились в C#.
2. Обновились сборки для ASP.NET Core, EntityFrameworkCore и Swagger (OpenAPI). Теперь используются версии NET6.
3. Удалены дубликаты контроллеров, которые демонстрировали использовать подхода Readonly/Writable (Calabonga.UnitOfWork.Controllers). Теперь остался только один пример, построенный на базе Mediatr.
4. Обновлена инфраструктура папок. Использована Vertical Slice Architecture, которую представил Jimmy Bogard.
5. Обновилось расширение VSIX-расширения для Visual Studio.

v5.0.8 от 2021-10-31
1. Обновились сборки для ASP.NET Core, EntityFrameworkCore и Swagger (OpenAPI). А также nuget-пакет FluentValidation и другие.
2. Конструкции некоторых методов переведин на lambda-выражения, чтобы следовать моде.
3. Обновилось расширение VSIX-расширения для Visual Studio.

v5.0.7 от 2021-09-06
1. Обновились сборки для ASP.NET Core, EntityFrameworkCore и Swagger (OpenAPI). А также nuget-пакеты Calabonga.Microservices.Core, FluentValidation и другие.
2. Добавлено несколько комментариев в код для обеспечения определенности в действиях
3. Обновилось расширение VSIX-расширения для Visual Studio.

v5.0.6 от 2021-09-08
1. Обновились сборки для ASP.NET Core и EntityFrameworkCore. А также nuget-пакеты OperationResultCore, Calabonga.Microservices.Core, Swagger, FluentValidation и другие.
2. Обновились метаданные, используемые для VSIX-расширения для Visual Studio.

v5.0.5 от 2021-06-04:
1. Исправлено несколько опечаток.
2. Обновились сборки для ASP.NET Core и EntityFrameworkCore. А также nuget-пакеты OperationResultCore, Calabonga.Microservices.Core, Swagger, FluentValidation и другие.

v5.0.4 от 2021-03-23:
1. Стандартный logger заменен на Serilog. Теперь читать логи стало проще.
2. Исправлено несколько опечаток.
3. Обновились сборки для ASP.NET Core и EntityFrameworkCore. А также nuget-пакеты OperationResultCore, Calabonga.Microservices.Core и другие.
4. Реализация Mediatr-запросов (Request) из классов превратились в записи (class -> record) С#.

v5.0.3 от 2021-03-16:
1. Удалена зависимость от некоторых сборок, которые вынесены на более высокий уровень. Это позволит обновлять их отдельно.
2. Исправлено несколько опечаток.
3. Обновились сборки для ASP.NET Core и EntityFrameworkCore до версий (5.0.4). А также nuget-пакеты OperationResultCore, Calabonga.Microservices.Core и другие.
4. Реализация Mediatr-запросов из контроллеров перенесены в проекты шаблонов из сборки Calabonga.AspNetCore.Controllers, чтобы примеры использования были доступны разработчику.

v5.0.2 от 2021-01-27:
1. Применены языковые *фишки* из C# 9.0. Другими словами, был неслабый рефакторинг кода для оптимизации.
2. Исправлено несколько опечаток.
3. Обновились сборки для `ASP.NET Core` и `EntityFramework Core` до версий (5.0.2). А также nuget-пакеты `OperationResultCore`, `Calabonga.Microservices.Core` и другие.

v5.0.1 от 2020-12-04:
1. В шаблоне микросервиса с IdentityServer4 и шаблоне без него удалены проекты xxx.Core. Файлы из этих проектов перенесены в проект xxx.Entities. Ссылки на Exceptions вынесены в nuget-пакет `Calabonga.Microservices.Core`.
2. В шаблоне микросервиса с IdentityServer4 добавлена возможность аутентификации как для Cookie типа, так и для Bearer. Да! Теперь настроено два типа аутентификации.
3. Применены языковые *фишки* из C# 8.0. Другими словами, был неслабый рефакторинг кода для оптимизации.
4. Исправлено множество опечаток.

v5.0.0 от 2020-11-15:
1. Все проекты во всех решениях (solutions) обновились до версии .NET 5.0.
2. Обновились nuget-сборки для `ASP.NET Core` и `EntityFramework Core` (5.0.0).
3. Добавилась реализация логирования, как пример использования, который описан на видео [ILogger в ASP.NET Core](https://youtu.be/09EVKgHgwnM)
4. На страницу API (`Swagger`) выведена информация о названии ветки (branch) и номер публикации (commit).

![Logo](/Whatnot/MicriserviceArchitecture.png)

# Документация
Документация доступна на GitHub в разделе [Wiki](https://github.com/Calabonga/Microservice-Template/wiki/Nimble-Microservice-Templates) (в режиме наполнения).

# Микросервисы
Шаблон (template) для Visual Studio для построения инфраструктуры микросервисов на базе ASP.NET Core.

# Microservices
Visual Studio project template for microservice module base on ASP.NET Core

# Скачать (Download)

[ASP NET Core 3.1](https://github.com/Calabonga/microservice-template/tree/master/Output/AspNetCore-v.3.1) - Шаблоны для версии ASP.NET Core 3.1

[ASP NET Core 5.0](https://github.com/Calabonga/microservice-template/tree/master/Output/AspNetCore-v.5.0) - Шаблоны для версии ASP.NET Core 5.0

[ASP NET Core 6.0](https://github.com/Calabonga/microservice-template/tree/master/Output/AspNetCore-v.6.0) - Шаблоны для версии ASP.NET Core 6.0

# Инструкции и дополнительные материалы

[Установка Nimble Templates](https://www.calabonga.net/blog/post/install-nimble-framework-version-5)

[Calabonga.AspNetCore.Controllers](https://github.com/Calabonga/Calabonga.AspNetCore.Controllers/) nuget-пакет на базе Mediatr

[Calabonga.UnitOfWork.Controllers](https://github.com/Calabonga/Calabonga.UnitOfWork.Controllers) nuget-пакет на base Readonly/Writable controllers

[Микросервисы: Шаблон для микросервиса](https://www.calabonga.net/blog/post/microservises-template)

[Микросервисы: Управление доступом](https://www.calabonga.net/blog/post/mikroservisy-3-centralizovannoe-upravlenie-dostupom)

[Микросервисы: Обмен данными между микросервисами](https://www.calabonga.net/blog/post/reshenie-obmen-dannym-mezhdu-mikroservisami)

[Микросервисы: Прокси для frontend](https://www.calabonga.net/blog/post/odin-frontend-dolzhen-rabotat-tolko-so-svoim-backend)

[Вопросы можно задать в блоге](https://www.calabonga.net/blog)

# Видео по версии 6.0

[Nimble Framework v6.1](https://youtu.be/xijBGwMEL8E)

[Nimble Framework v6.0](https://youtu.be/euOLhhNEcwg)

# Видео по версии 3.1 (3.0)

Часть 0. [Nimble: Установка шаблонов для микросервисов](https://youtu.be/rc0wvL0jlzc)

Часть 1. [Nimble: Демонстрация шаблона микросервиса на примере](https://youtu.be/N0dRGGV2iEg)

Часть 2. [Nimble: Установка шаблонов для микросервисов](https://youtu.be/rc0wvL0jlzc)

Часть 3. [Nimble: Основные и базовые понятия](https://youtu.be/WbSwp1Aa7hM)

Часть 4. [Nimble: Создаем свой Query и Handler для IMediator](https://youtu.be/wUfT5aLHVV8)

[Микросервисы 2](https://www.youtube.com/playlist?list=PLIB8be7sunXMh9dckizdXz65hLX_HRzlc) (плей-лист)

# Видео по версии 2.2

[Микросервисы](https://www.youtube.com/playlist?list=PLIB8be7sunXMXTZKptqEtXAACpsYZdzi_) (плей-лист)

# Visual Studio Extensions

Теперь шаблоны для генерации микросервисов доступны также и в Visual Studio Marketplace

![marketplace](https://github.com/Calabonga/Microservice-Template/blob/master/Whatnot/vs-extension-market-view.png)

[Посмотреть marketplace](https://marketplace.visualstudio.com/items?itemName=Calabonga.microserivce-templates)

Также можно установить прямо из Visual Studio 
![extension](https://github.com/Calabonga/Microservice-Template/blob/master/Whatnot/vs-extension.png)

# Дополнительные материалы по Nimble Framework

[Микросервисы: Nimble Framework v.2](https://youtu.be/bTxruvbhDss)

[Nimble: Создание микросервиса](https://youtu.be/7nw8Naxf2U0)

Nimble Framework для NET6 Готовится

# Благодарности

Благодарности помощь каналу принимаются (Support This Project):
* https://www.calabonga.net/site/thanks
