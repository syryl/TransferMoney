using dciSphere.Core.Models;
using dciSphere.Abstractions.Repository;
using dciSphere.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using dciSphere.Infrastructure.Events;
using dciSphere.Abstraction.Events;
using dciSphere.Abstraction.Application;
using dciSphere.Infrastructure.Logging;
using MediatR;
using FluentValidation;
using dciSphere.Infrastructure.Validation;
using Microsoft.Extensions.Caching.Memory;
using dciSphere.Abstraction.Cache;
using dciSphere.Infrastructure.Cache;
using dciSphere.Infrastructure.Auth;

namespace dciSphere.Infrastructure;
public static class Startup
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration, AppStartupOptions options)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services
            .AddDbContext<DatabaseContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString);
                //options.UseInMemoryDatabase("dciSphereDb");
            });
        var sp = services.BuildServiceProvider();
        var dbContext = sp.GetService<DatabaseContext>()!;
        if (options.UsingLoggingBehavior)
            AddLoggingBehavior(services);
        if (options.UsingValidation)
            AddValidationBehavior(services);
        AddRepositoryToDI<Bank>(services, dbContext);
        AddRepositoryToDI<Account>(services, dbContext);

        AddEvents(services);
        AddCaching(services);
        services.AddAuth(configuration);
    }

    private static void AddRepositoryToDI<TEntity>(IServiceCollection services, DbContext dbContext) where TEntity : class
    {
        var repo = new Repository<TEntity>(dbContext);
        var vrepo = new ViewRepository<TEntity>(dbContext);
        services
            .AddScoped<IRepository<TEntity>>(_ => repo)
            .AddScoped<IViewRepository<TEntity>>(_ => vrepo);
    }

    private static void AddEvents(IServiceCollection services)
    {
        services.AddSingleton<IEventDispatcher, EventDispatcher>();
        services.AddHostedService<EventConsumer>();
    }


    private static void AddLoggingBehavior(IServiceCollection services)
    {
        services
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
    }

    private static void AddValidationBehavior(IServiceCollection services)
    {
        services
            .AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
    }

    public static T BindSettings<T>(this IServiceCollection services, IConfiguration configuration, string section) where T : class, new()
    {
        T settings = new T();
        configuration.GetSection(section).Bind(settings);
        services.AddSingleton(settings);
        return settings;
    }
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : new()
    {
        var options = new T();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }

    public static IReadOnlyList<T> GetOptionsList<T>(this IConfiguration configuration, string sectionName) where T : new()
    {
        var options = new List<T>();
        configuration.GetSection(sectionName).Bind(options);
        return options;
    }
    public static T GetOptions<T>(this IServiceCollection services, string sectionName) where T : new()
    {
        using var serviceProvider = services.BuildServiceProvider();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        return configuration.GetOptions<T>(sectionName);
    }

    public static void AddCaching(IServiceCollection services)
    {
        services.AddScoped(typeof(ICacheRepository), typeof(InMemoryCacheProvider));
        services.AddMemoryCache();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
    }
}
