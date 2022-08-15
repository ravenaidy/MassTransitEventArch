using System;

namespace MassTransit.AccountOrchestrator.Events.Login
{
    public record LoginRequest(Guid CorrelationId, string Username, string Password) : CorrelatedBy<Guid>;
}
