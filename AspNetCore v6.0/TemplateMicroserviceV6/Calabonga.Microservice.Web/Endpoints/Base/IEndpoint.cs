namespace $safeprojectname$.Endpoints.Base
{
    public interface IEndpoint
    {
        void ConfigureApplication(WebApplication app);

        void ConfigureServices(IServiceCollection services, IConfiguration configuration);
    }
}
