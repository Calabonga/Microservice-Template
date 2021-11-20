namespace $safeprojectname$.Endpoints.Base;

public static class EndpointExtensions
{
    public static void AddEndpointsToMicroservice(this IServiceCollection source, WebApplicationBuilder builder, params Type[] entryPointsAssembly)
    {
        var endpoints = new List<IEndpoint>();

        foreach (var entryPoint in entryPointsAssembly)
        {
            var types = entryPoint.Assembly.ExportedTypes.Where(x => !x.IsAbstract && typeof(IEndpoint).IsAssignableFrom(x));
            var instances = types.Select(Activator.CreateInstance).Cast<IEndpoint>();
            endpoints.AddRange(instances);
        }

        endpoints.ForEach(endpoint => endpoint.ConfigureServices(source, builder.Configuration));
        source.AddSingleton(endpoints as IReadOnlyCollection<IEndpoint>);
    }

    public static void UseEndpointsInMicroservice(this WebApplication source)
    {
        var endpoints = source.Services.GetRequiredService<IReadOnlyCollection<IEndpoint>>();
        foreach (var endpoint in endpoints)
        {
            endpoint.ConfigureApplication(source);
        }
    }
}
