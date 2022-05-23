using System;
using MassTransit.LoginService.Models;
using MassTransit.Shared.Infrastructure.AutoMapperExtensions.Contracts;

namespace MassTransit.LoginService.Events
{
    public record CreateLogin(Guid CorrelationId, string Username, string Password) : CorrelatedBy<Guid>, IMapTo<Login>;
}