using System;
using Destructurama.Attributed;
using MassTransit.LoginService.Models;
using MassTransit.Shared.Infrastructure.AutoMapperExtensions.Contracts;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.LoginService.Events;

public class CreateLogin : IEvent, IMapTo<Login>
{
    public Guid LoginId { get; set; }

    public string Username { get; set; }

    [NotLogged]
    public string Password { get; set; }

    [NotLogged]
    public Guid CorrelationId { get; set; }
}
