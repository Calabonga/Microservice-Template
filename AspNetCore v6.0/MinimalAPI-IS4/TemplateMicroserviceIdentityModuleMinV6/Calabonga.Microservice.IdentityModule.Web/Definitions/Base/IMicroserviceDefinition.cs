namespace $safeprojectname$.Definitions.Base
{
    /// <summary>
    /// Microservice definition interface abstraction
    /// </summary>
    internal interface IMicroserviceDefinition
    {
        /// <summary>
        /// Configure services for current microservice
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        void ConfigureServices(IServiceCollection services, IConfiguration configuration);

        /// <summary>
        /// Configure application for current microservice
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        void ConfigureApplication(IApplicationBuilder app, IWebHostEnvironment env);
    }
}