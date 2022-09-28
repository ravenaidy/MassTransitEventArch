using MassTransit.Orchestrator.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MassTransit.Orchestrator.Dependencies
{
    public static class ServiceExtensions 
    {
        public static IServiceCollection AddLoginStateMachineSettings(this IServiceCollection services, IConfiguration configuration)
        {
            var loginStateMachineSettings = new LoginStateMachineSettings();
            configuration.GetSection("LoginStateMachineSettings").Bind(loginStateMachineSettings);
            services.AddSingleton(loginStateMachineSettings);

            return services;
        }
    }
}
