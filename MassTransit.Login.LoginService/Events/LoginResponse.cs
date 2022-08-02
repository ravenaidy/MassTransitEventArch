using MassTransit.LoginService.Models;
using MassTransit.Shared.Infrastructure.AutoMapperExtensions.Contracts;

namespace MassTransit.LoginService.Events
{
    public class LoginResponse : IMapFrom<Login>
    {
        public int LoginId { get; set; }
        public string Username { get; set; }
    }
}