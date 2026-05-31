using Inkukan.Application.Mediator;
using Inkukan.Application.Mediator.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Inkukan.Application.Mediator.Extensions;

public static class Mediator
{
    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly assembly)
    {
        services.AddScoped<IInkukaMediator, InkukaMediator>();

        System.Type handlerInterfaceWithResult = typeof(IRequestHandler<,>);
        System.Type handlerInterfaceWithoutResult = typeof(IRequestHandler<>);
        System.Type[] types = assembly.GetTypes();
        var handlers = types
            .Where(t => !t.IsAbstract && !t.IsInterface && !t.IsGenericType)
            .SelectMany(t => t.GetInterfaces()
                .Where(i => i.IsGenericType && (i.GetGenericTypeDefinition() == handlerInterfaceWithResult || i.GetGenericTypeDefinition() == handlerInterfaceWithoutResult))
                .Select(i => new { Interface = i, Implementation = t }));

        foreach (var h in handlers)
            services.AddScoped(h.Interface, h.Implementation);

        return services;
    }
}