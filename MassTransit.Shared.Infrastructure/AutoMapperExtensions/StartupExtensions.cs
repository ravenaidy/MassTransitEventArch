﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace MassTransit.Shared.Infrastructure.AutoMapperExtensions
{
    public static class StartupExtensions
    {
        private static readonly HashSet<Assembly> _assemblies = new();

        internal static ReadOnlyCollection<Assembly> Assemblies => _assemblies.ToList().AsReadOnly();

        /// <summary>
        /// Adds <seealso cref="AutoMapper"/> to the Dependency Injection container.
        /// It scans the specified assemblies for types that derive from <seealso cref="Profile"/>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assemblies"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static IServiceCollection AddObjectMapping(this IServiceCollection services,
            params Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                _assemblies.Add(assembly);
            }

            _assemblies.Add(Assembly.GetCallingAssembly());
            _assemblies.Add(typeof(AutoMapperProfile).Assembly);

            return services.AddAutoMapper(Assemblies);
        }
    }
}