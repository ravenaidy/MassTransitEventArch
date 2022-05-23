using System;

namespace MassTransit.LoginService.Events
{
    public record LoginCreated(int UserId, DateTime Timestamp);
}