using MassTransit.AccountOrchestrator.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MassTransit.AccountOrchestrator.Dependencies
{
    public static class ServiceExtensions 
    {
        public static IServiceCollection AddLoginStateMachineSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var loginStateMachineSettings = new LoginStateMachineSettings();
            configuration.GetSection("LoginStateMachineSettings").Bind(loginStateMachineSettings);
            services.AddSingleton<LoginStateMachineSettings>(loginStateMachineSettings);

            return services;
        }
    }
}
