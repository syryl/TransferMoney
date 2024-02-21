using dciSphere.Abstraction.Events;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace dciSphere.Messages;
public static class Startup
{
    public static void AddMessages(this IServiceCollection services)
    {
        var handlersTypes = Assembly.GetExecutingAssembly().GetTypes().Where(x => !x.IsInterface && x.GetInterfaces().Any(i => i.IsGenericType && typeof(IEventHandler<>).IsAssignableFrom(i.GetGenericTypeDefinition())));
        foreach (var handlerType in handlersTypes)
        {
            var interfaceType = handlerType.GetInterfaces().First();
            services.AddScoped(interfaceType, handlerType);
        }
    }
}
