namespace $safeprojectname$.Endpoints.Base
{
    public static class EndpointExtensions
    {
        public static void AddEndpointsToMicroservice(this IServiceCollection source, WebApplicationBuilder builder, params Type[] entryPointsAssembly)
        {
            var endpoints = new List<IEndpointDefinition>();

            foreach (var entryPoint in entryPointsAssembly)
            {
                var types = entryPoint.Assembly.ExportedTypes.Where(x => !x.IsAbstract && typeof(IEndpointDefinition).IsAssignableFrom(x));
                var instances = types.Select(Activator.CreateInstance).Cast<IEndpointDefinition>();
                endpoints.AddRange(instances);
            }

            endpoints.ForEach(endpoint => endpoint.ConfigureServices(source, builder.Configuration));
            source.AddSingleton(endpoints as IReadOnlyCollection<IEndpointDefinition>);
        }

        public static void UseEndpointsInMicroservice(this WebApplication source)
        {
            var endpoints = source.Services.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>();
            foreach (var endpoint in endpoints)
            {
                endpoint.ConfigureApplication(source);
            }
        }
    }
}