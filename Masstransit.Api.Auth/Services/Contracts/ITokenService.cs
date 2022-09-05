using System.Threading.Tasks;
using Masstransit.Api.Auth.DTO;

namespace Masstransit.Api.Auth.Services.Contracts
{
    public interface ITokenService
    {
        Task<string> GenerateToken(Login login);
    }
}
