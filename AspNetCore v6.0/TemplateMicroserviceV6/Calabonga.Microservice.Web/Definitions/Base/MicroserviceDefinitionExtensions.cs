namespace $safeprojectname$.Definitions.Base;

public static class MicroserviceDefinitionExtensions
{
    public static void AddMicroserviceDefinitions(this IServiceCollection source, WebApplicationBuilder builder, params Type[] entryPointsAssembly)
    {
        var definitions = new List<IMicroserviceDefinition>();

        foreach (var entryPoint in entryPointsAssembly)
        {
            var types = entryPoint.Assembly.ExportedTypes.Where(x => !x.IsAbstract && typeof(IMicroserviceDefinition).IsAssignableFrom(x));
            var instances = types.Select(Activator.CreateInstance).Cast<IMicroserviceDefinition>();
            definitions.AddRange(instances);
        }

        definitions.ForEach(microservice => microservice.ConfigureServices(source, builder.Configuration));
        source.AddSingleton(definitions as IReadOnlyCollection<IMicroserviceDefinition>);
    }

    public static void UseMicroserviceDefinitions(this WebApplication source)
    {
        var definitions = source.Services.GetRequiredService<IReadOnlyCollection<IMicroserviceDefinition>>();
        var environment = source.Services.GetRequiredService<IWebHostEnvironment>();
        foreach (var endpoint in definitions)
        {
            endpoint.ConfigureApplication(source, environment);
        }
    }

}
