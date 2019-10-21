# Nimble Framework

Nimble Framework - это набор шаблонов для построения микросервисной архитектуры на платформе ASP.NET Core. На данный момент существуют версии для ASP.NET Core 2.2 и ASP.NET Core 3.0.

Nimble Framework (далее Nimble) позволяет создавать из шаблонов готовые к использованию приложения API для дальнейшего использования либо как самостоятельный backend для Single Page Application (SPA), либо как часть микросервисной архитектуры.

# История

Nimble Framework появился в виду того, что частое создание микросервисов занимает много времени. Особенно, когда эти микросервисы объеденины в одну архитектуру и поэтому должны быть связаны между собой. Для облегчения задачи "введение нового сервиса в систему" и были придуманы шаблоны для их создания.

Nimble Framework представляет собой набор из двух шаблонов для Visual Studio для создания проектов ASP.NET Core.

# Ключевые моменты

1. [Установка шаблонов](./Nimble/О фреймворке/)

2. [Микросервис с сервером авторизации (IdentityServer4)](https://github.com/Calabonga/Microservice-Template/wiki/%D0%A2%D0%B8%D0%BF%D1%8B-%D1%88%D0%B0%D0%B1%D0%BB%D0%BE%D0%BD%D0%BE%D0%B2)

   - [Создание сервиса](https://github.com/Calabonga/Microservice-Template/wiki/%D0%A1%D0%BE%D0%B7%D0%B4%D0%B0%D0%BD%D0%B8%D0%B5-%D0%BC%D0%B8%D0%BA%D1%80%D0%BE%D1%81%D0%B5%D1%80%D0%B2%D0%B8%D1%81%D0%B0-%D0%B8%D0%B7-%D1%88%D0%B0%D0%B1%D0%BB%D0%BE%D0%BD%D0%B0)
   - [Проекты и их предназначения](https://github.com/Calabonga/Microservice-Template/wiki/%D0%9F%D1%80%D0%BE%D0%B5%D0%BA%D1%82%D1%8B-%D0%B8-%D0%B8%D1%85-%D0%BF%D1%80%D0%B5%D0%B4%D0%BD%D0%B0%D0%B7%D0%BD%D0%B0%D1%87%D0%B5%D0%BD%D0%B8%D0%B5)
   - [Стартовая страница Swagger](https://github.com/Calabonga/Microservice-Template/wiki/%D0%A1%D1%82%D0%B0%D1%80%D1%82%D0%BE%D0%B2%D0%B0%D1%8F-%D1%81%D1%82%D1%80%D0%B0%D0%BD%D0%B8%D1%86%D0%B0-Swagger)
   - [Файлы, папки и их предназначение в проектах](https://github.com/Calabonga/Microservice-Template/wiki/%D0%A4%D0%B0%D0%B9%D0%BB%D1%8B,-%D0%BF%D0%B0%D0%BF%D0%BA%D0%B8-%D0%B8-%D0%B8%D1%85-%D0%BF%D1%80%D0%B5%D0%B4%D0%BD%D0%B0%D0%B7%D0%BD%D0%B0%D1%87%D0%B5%D0%BD%D0%B8%D0%B5-%D0%B2-%D0%BF%D1%80%D0%BE%D0%B5%D0%BA%D1%82%D0%B0%D1%85)
   - [Предопреденные данные (Seed)](<https://github.com/Calabonga/Microservice-Template/wiki/%D0%9F%D1%80%D0%B5%D0%B4%D0%BE%D0%BF%D1%80%D0%B5%D0%B4%D0%B5%D0%BD%D0%BD%D1%8B%D0%B5-%D0%B4%D0%B0%D0%BD%D0%BD%D1%8B%D0%B5-(Seed)>)
   - [Файл Setup.cs](https://github.com/Calabonga/Microservice-Template/wiki/%D0%A4%D0%B0%D0%B9%D0%BB-Setup.cs)
   - [Engine](https://github.com/Calabonga/Microservice-Template/wiki/Engine)
   - [AutoMapper (MapperRegistration unit-tests)](https://github.com/Calabonga/Microservice-Template/wiki/%D0%98%D1%81%D0%BF%D0%BE%D0%BB%D1%8C%D0%B7%D0%BE%D0%B2%D0%B0%D0%BD%D0%B8%D0%B5-Automapper)
   - [Настройки приложения CurrentAppSettings](https://github.com/Calabonga/Microservice-Template/wiki/%D0%9D%D0%B0%D1%81%D1%82%D1%80%D0%BE%D0%B9%D0%BA%D0%B8-%D0%BF%D1%80%D0%B8%D0%BB%D0%BE%D0%B6%D0%B5%D0%BD%D0%B8%D1%8F-CurrentAppSettings)
   - [Контроллеры и их методы](https://github.com/Calabonga/Microservice-Template/wiki/%D0%9A%D0%BE%D0%BD%D1%82%D1%80%D0%BE%D0%BB%D0%BB%D0%B5%D1%80%D1%8B-%D0%B8-%D0%B8%D1%85-%D0%BC%D0%B5%D1%82%D0%BE%D0%B4%D1%8B)
   - [Аутентификация и авторизация](https://github.com/Calabonga/Microservice-Template/wiki/%D0%90%D1%83%D1%82%D0%B5%D0%BD%D1%82%D0%B8%D1%84%D0%B8%D0%BA%D0%B0%D1%86%D0%B8%D1%8F-%D0%B8-%D0%B0%D0%B2%D1%82%D0%BE%D1%80%D0%B8%D0%B7%D0%B0%D1%86%D0%B8%D1%8F)
   - [Пример создание сущности Product](https://github.com/Calabonga/Microservice-Template/wiki/%D0%9F%D1%80%D0%B8%D0%BC%D0%B5%D1%80-%D1%81%D0%BE%D0%B7%D0%B4%D0%B0%D0%BD%D0%B8%D0%B5-%D1%81%D1%83%D1%89%D0%BD%D0%BE%D1%81%D1%82%D0%B8-Product)

3. [Микросервис](https://github.com/Calabonga/Microservice-Template/wiki/%D0%A2%D0%B8%D0%BF%D1%8B-%D1%88%D0%B0%D0%B1%D0%BB%D0%BE%D0%BD%D0%BE%D0%B2)

   [Создание сервиса](https://github.com/Calabonga/Microservice-Template/wiki/%D0%A1%D0%BE%D0%B7%D0%B4%D0%B0%D0%BD%D0%B8%D0%B5-%D0%BC%D0%B8%D0%BA%D1%80%D0%BE%D1%81%D0%B5%D1%80%D0%B2%D0%B8%D1%81%D0%B0-%D0%B8%D0%B7-%D1%88%D0%B0%D0%B1%D0%BB%D0%BE%D0%BD%D0%B0)

# Версия

Текущая версия Nimble 1.0.0. Документация описывает версию ASP.NET Core 3.0.
