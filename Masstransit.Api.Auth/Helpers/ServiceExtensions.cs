namespace Masstransit.Api.Auth.Helpers
{
    public static class ServiceExtensions
    {
        public static void TokenConfiguration(this IServiceCollection service, IConfiguration configuration)
        {
            var jwtTokenConfiguration = new JWTConfiguration();
            configuration.GetSection("JWTConfig").Bind(jwtTokenConfiguration);
            service.AddSingleton(jwtTokenConfiguration);
        }
    }
}
