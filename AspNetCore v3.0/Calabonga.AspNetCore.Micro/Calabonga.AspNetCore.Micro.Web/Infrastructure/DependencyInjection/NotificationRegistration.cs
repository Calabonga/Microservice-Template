using Microsoft.Extensions.DependencyInjection;

namespace Calabonga.AspNetCore.Micro.Web.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Dependency Injection Registration
    /// </summary>
    public partial class DependencyContainer
    {
        /// <summary>
        /// Registering Notification infrastructure
        /// </summary>
        /// <param name="services"></param>
        public static void Notifications(IServiceCollection services)
        {
            //// notification infrastructure items
            //services.AddTransient<INotificationService, NotificationService>();
            //services.AddTransient<INotificationDirector, NotificationDirector>();
            //services.AddTransient<INotificationProcessor, NotificationProcessor>();
            //services.AddTransient<INotificationFactory, NotificationFactory>();

            //// template loaders
            //services.AddTransient<EmailTemplateLoader>();

            //// notification builders
            //services.AddTransient<EmailNotificationBuilder>();

            //// email notification viewModel for email body rendering
            //Assembly.GetExecutingAssembly().GetTypesAssignableFrom<EmailNotificationModelBase>().ForEach(t => services.AddTransient(typeof(EmailNotificationModelBase), t));

            //// senders protocols implementations
            //services.AddTransient<INotificationEmailSender, NotificationEmailSender>();

            //// broadcast communication sender
            //services.AddTransient<IMessageSender, MessageSenderEmail>();
        }
    }
}
