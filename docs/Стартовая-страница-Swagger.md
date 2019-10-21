## Версия
Страница описана для версии ASP.NET Core 3.0.

## Swagger
В проект интегрирован [Swagger](https://swagger.io) для возможности выполнения запросов к API через web-интерфейс 

![swagger interface](https://github.com/Calabonga/Microservice-Template/blob/master/Whatnot/Screenshots/create-from-template-8.png)

## Настройки Swagger
В проекте .Web\AppStart\ConfigureServices\ в файле ConfigureServicesSwagger.cs находятся настройки Swagger.

## Конфигурация Swagger
Кофигурация Swagger возможно в методе SwaggerSettings:
```
/// <summary>
/// Set up some properties for swagger UI
/// </summary>
/// <param name="settings"></param>
public static void SwaggerSettings(SwaggerUIOptions settings)
{
    settings.SwaggerEndpoint(SwaggerConfig, $"{AppTitle} v.{AppVersion}");
    settings.RoutePrefix = SwaggerUrl;
    settings.DocumentTitle = "Microservice API";
    settings.DefaultModelExpandDepth(0);
    settings.DefaultModelRendering(ModelRendering.Model);
    settings.DefaultModelsExpandDepth(0);
    settings.DocExpansion(DocExpansion.None);
    settings.OAuthClientId("microservice1");
    settings.OAuthScopeSeparator(" ");
    settings.OAuthClientSecret("secret");
    settings.DisplayRequestDuration();
    settings.OAuthAppName("Microservice API (with IdentityServer4) module API documentation");
}
```

## Комментарии из Summary

Для того чтобы swagger смог читать описания ваших методов из Summary, необходимо включить в настройках проекта генерацию XML-документации и добавить в `services.AddSwaggerGen()` следующие строки:
```
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
```