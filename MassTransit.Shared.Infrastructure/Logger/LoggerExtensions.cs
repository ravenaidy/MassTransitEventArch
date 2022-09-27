using System;
using Serilog.Context;

namespace MassTransit.Shared.Infrastructure.Logger;

public static partial class LoggerExtensions
{
  private static void Log(Action action, string correlationId)
  {
    using (LogContext.PushProperty("CorrelationId", correlationId))
    {
      action();
    }
  }
}