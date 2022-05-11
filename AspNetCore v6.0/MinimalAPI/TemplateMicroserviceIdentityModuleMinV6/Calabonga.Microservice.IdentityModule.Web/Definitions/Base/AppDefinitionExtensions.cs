namespace $safeprojectname$.Definitions.Base;

public static class AppDefinitionExtensions
{
    public static void AddDefinitions(this IServiceCollection source, WebApplicationBuilder builder, params Type[] entryPointsAssembly)
    {
        var definitions = new List<IAppDefinition>();

        foreach (var entryPoint in entryPointsAssembly)
        {
            var types = entryPoint.Assembly.ExportedTypes.Where(x => !x.IsAbstract && typeof(IAppDefinition).IsAssignableFrom(x));
            var instances = types.Select(Activator.CreateInstance).Cast<IAppDefinition>();
            definitions.AddRange(instances);
        }

        definitions.ForEach(app => app.ConfigureServices(source, builder.Configuration));
        source.AddSingleton(definitions as IReadOnlyCollection<IAppDefinition>);
    }

    public static void UseDefinitions(this WebApplication source)
    {
        var definitions = source.Services.GetRequiredService<IReadOnlyCollection<IAppDefinition>>();
        var environment = source.Services.GetRequiredService<IWebHostEnvironment>();
        foreach (var endpoint in definitions)
        {
            endpoint.ConfigureApplication(source, environment);
        }
    }

}