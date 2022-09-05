using System;
using Destructurama.Attributed;

namespace MassTransit.Shared.Infrastructure.Events;

public interface IEvent  
{
    [NotLogged]
    public Guid CorrelationId { get; set; }
}
