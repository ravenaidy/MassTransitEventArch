using Masstransit.Api.Auth.Events;

namespace Masstransit.Api.Auth.Services.Contracts
{
    public interface ITokenService
    {
        Task<string> GenerateToken(GetAuthToken tokenRequest);
    }
}
