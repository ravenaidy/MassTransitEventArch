using System;
using Destructurama.Attributed;
using MassTransit.Shared.Infrastructure.Events;
using MediatR;

namespace MassTransit.BFFServices.SignalRWorker.Account.Commands;

public class NewAccountRequest : IRequest, IEvent
{
    public Guid UserId { get; set; } = Guid.NewGuid();

    public string Username { get; set; }

    [NotLogged]
    public string Password { get; set; }

    [NotLogged]
    public string Email { get; set; }

    [NotLogged]
    public string Firstname { get; set; }

    [NotLogged]
    public string Lastname { get; set; }

    [NotLogged]
    public int PhoneNumber { get; set; }

    public int Gender { get; set; }

    [NotLogged]
    public string AddressLine1 { get; set; }

    [NotLogged]
    public string AddressLine2 { get; set; }

    [NotLogged]
    public string AddressLine3 { get; set; }

    [NotLogged]
    public string City { get; set; }

    [NotLogged]
    public string Country { get; set; }

    [NotLogged]
    public int PostalCode { get; set; }
    
    [NotLogged]
    public Guid CorrelationId { get; set; } 
}
