# Внимание
Перед выбором версии платформы, пожалуйста, ознакомьтесь с  [изменениями в шаблонах](https://github.com/Calabonga/Microservice-Template/wiki/%D0%98%D0%B7%D0%BC%D0%B5%D0%BD%D0%B5%D0%BD%D0%B8%D1%8F-%D0%B2-%D1%88%D0%B0%D0%B1%D0%BB%D0%BE%D0%BD%D0%B0%D1%85)

# Версии шаблонов (ASP.NET Core 3.1)

v.2.0.2 от 25.05.2020:
1. Установлена сборка `Microsoft.EntityFrameworkCore.InMemory`, чтобы можно было вновь созданный проект запустить без подключения к базе данных. В этой связи обновлена конфигурация сервиса.
2. Обновились сборки для ASP.NET Core и EntityFramework Core (3.1.4)
3. Обновились все сборки от Swagger до версии 5.4.1
4. Добавлен новый файл `ConfigureServicesAuthentication` в папке AppStart, и, соответственно, все настройки аутентификации и авторизации перенесены в него.

v.2.0.1 от 14.04.2020:
1. Обновились сборки для `ASP.NET Core` и `EntityFramework Core` (3.1.3)
2. Обновились все сборки от `Swagger` до версии 5.3.1
3. Обновились `Calabonga.UnitOfWork.Controllers` до версии 1.1.3

v.2.0.0 от 13.03.2020:
1. Обновились сборки для `ASP.NET Core` и `EntityFramework Core` (3.1.2)
2. Обновился `Swagger` до версии 5.1.0


v2.0.0-beta1 от 02.02.2020:
1. Обновился `WritableController`, теперь он принимает в конструктор `IEntityManagerFactory`.
2. Обновились сборки для `ASP.NET Core` и `EntityFramework Core` (3.1.1)
3. Обновился `Swagger` до версии 5.0.0

![Logo](/Whatnot/MicriserviceArchitecture31.png)

>Винимание: Шаблоны (с сервером авторизации и без него) для версии ASP.NET Core 2.2 и ASP.NET Core 3.0 остаются также доступны для загрузки. 

![Logo](/Whatnot/MicriserviceArchitecture.png)

# Документация
Документация доступна на GitHub в разделе [Wiki](https://github.com/Calabonga/Microservice-Template/wiki/Nimble-Microservice-Templates) (в режиме наполнения).

# Микросервисы
Шаблон (template) для Visual Studio для построения инфраструктуры микросервисов на базе ASP.NET Core.

# Microservices
Visual Studio project template for microservice module base on ASP.NET Core

# Скачать (Download)
[ASP.NET Core 2.2](https://github.com/Calabonga/microservice-template/tree/master/Output/AspNetCore-v.2.2) - Шаблоны для версии ASP.NET Core 2.2

[ASP NET Core 3.0](https://github.com/Calabonga/microservice-template/tree/master/Output/AspNetCore-v.3.0) - Шаблоны для версии ASP.NET Core 3.0

[ASP NET Core 3.1](https://github.com/Calabonga/microservice-template/tree/master/Output/AspNetCore-v.3.1) - Шаблоны для версии ASP.NET Core 3.1

# Инструкции и дополнительные материалы

[Микросервисы: Шаблон для микросервиса](https://www.calabonga.net/blog/post/microservises-template)

[Микросервисы: Управление доступом](https://www.calabonga.net/blog/post/mikroservisy-3-centralizovannoe-upravlenie-dostupom)

[Микросервисы: Обмен данными между микросервисами](https://www.calabonga.net/blog/post/reshenie-obmen-dannym-mezhdu-mikroservisami)

[Микросервисы: Прокси для frontend](https://www.calabonga.net/blog/post/odin-frontend-dolzhen-rabotat-tolko-so-svoim-backend)

[Вопросы можно задать в блоге](https://www.calabonga.net/blog)

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

Также можно установить прямо из Visual Studio

![extension](https://github.com/Calabonga/Microservice-Template/blob/master/Whatnot/vs-extension.png)
