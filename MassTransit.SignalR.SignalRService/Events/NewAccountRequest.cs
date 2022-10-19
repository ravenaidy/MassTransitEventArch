using System;
using Destructurama.Attributed;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.SignalR.SignalRService.DTO;

public class NewAccountRequest : IEvent
{
    public string Username { get; set; }

    [NotLogged]
    public string Password { get; set; }

    [NotLogged]
    public string Email { get; set; }

    [NotLogged]
    public string Firstname { get; set; }

    [NotLogged]
    public string Lastname { get; set; }

    public int Gender { get; set; }

    [NotLogged]
    public int PhoneNumber { get; set; }

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
    public Guid CorrelationId { get; set; } = Guid.NewGuid();
}
