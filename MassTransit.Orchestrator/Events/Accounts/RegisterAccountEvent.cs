using System;
using Destructurama.Attributed;
using MassTransit.AccountOrchestrator.Models;
using MassTransit.Shared.Infrastructure.Events;

namespace MassTransit.Orchestrator.Events.Accounts;

public class RegisterAccount : IEvent
{
    public Guid UserId { get; set; }
    
    [NotLogged]
    public string Email { get; set; }
    public string Username { get; set; }

    [NotLogged]
    public string Password { get; set; }

    [NotLogged]
    public string Firstname { get; set; }

    [NotLogged]
    public string Lastname { get; set; }

    [NotLogged]
    public int PhoneNumber { get; set; }

    public Gender Gender { get; set; }

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