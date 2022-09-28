using System;

namespace MassTransit.Shared.Infrastructure.Extensions
{
  public static class GenericEnrichment
  {
    public static TOutput Enrich<TInput, TOutput>(this TInput obj, Func<TInput, TOutput> func) => func(obj);
  }
}
