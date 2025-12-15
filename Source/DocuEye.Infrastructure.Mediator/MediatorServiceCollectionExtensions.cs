using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.Infrastructure.Mediator.Events;
using DocuEye.Infrastructure.Mediator.Queries;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DocuEye.Infrastructure.Mediator
{
    public static class MediatorServiceCollectionExtensions
    {
        public static IServiceCollection AddMediator(this IServiceCollection services, Action<MediatorOptions> configureOptions)
        {
            var options = new MediatorOptions();
            configureOptions(options);

            services.AddSingleton<IMediator, Mediator>();

            foreach (var assembly in options.GetAssemblies())
            {
                services.AddHandlersFrom(assembly);
            }

            return services;
        }

        internal static IServiceCollection AddHandlersFrom(this IServiceCollection services, Assembly assembly)
        {
            var ifaceOpenGenerics = new[]
            {
                typeof(ICommandHandler<>),        // single-arg command
                typeof(ICommandHandler<,>),       // command + result
                typeof(IEventHandler<>),          // event
                typeof(IQueryHandler<,>)          // query + result
            };

            foreach (var type in GetLoadableTypes(assembly))
            {
                if (type.IsAbstract || type.IsInterface) continue;

                if (type.IsGenericTypeDefinition)
                {
                    foreach (var openIface in ifaceOpenGenerics)
                    {
                        if (ImplementsOpenGeneric(type, openIface))
                            services.AddTransient(openIface, type);
                    }
                    continue;
                }

                // Register closed generic implementations for all matching service interfaces
                var serviceInterfaces = type.GetInterfaces()
                    .Where(i => i.IsGenericType && ifaceOpenGenerics.Contains(i.GetGenericTypeDefinition()));

                foreach (var serviceType in serviceInterfaces)
                    services.AddTransient(serviceType, type);
            }
            
            return services;
        }

        private static bool ImplementsOpenGeneric(Type type, Type openGenericInterface)
        {
            // Check if the type implements the open generic interface definition
            return type.GetInterfaces().Any(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == openGenericInterface);
        }

        private static IEnumerable<Type> GetLoadableTypes(Assembly asm)
        {
            try { return asm.GetTypes(); }
            catch (ReflectionTypeLoadException ex) { return ex.Types.Where(t => t != null)!; }
        }
    }
}
