using System;
using Destructurama.Attributed;
using MassTransit.LoginService.Models;
using MassTransit.Shared.Infrastructure.AutoMapperExtensions.Contracts;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.LoginService.Events;

public class LoginResponse : IMapFrom<Login>, IEvent
{
    public Guid LoginId { get; set; }
    public string Username { get; set; }
    [NotLogged]
    public Guid CorrelationId { get; set; }
}
