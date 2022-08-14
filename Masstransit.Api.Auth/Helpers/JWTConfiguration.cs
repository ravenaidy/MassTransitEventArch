namespace Masstransit.Api.Auth.Helpers
{
    public class JWTConfiguration
    {
        public string Key { get; set; }
        public string Subject { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}