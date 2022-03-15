using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MinimalBlog.Api.Contracts;
using MinimalBlog.Dal;

namespace MinimalBlog.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        WebApplicationBuilder builder)
    {
        if (builder == null)
        {
            throw new ArgumentNullException(nameof(builder));
        }

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = builder.Environment.ApplicationName, Version = "v1" });
            options.TagActionsBy(ta => new List<string> { ta.ActionDescriptor.DisplayName! });
        });

        var connectionString = builder.Configuration.GetConnectionString("Default");
        builder.Services.AddDbContext<MinimalBlogDbContext>(opt => opt.UseSqlServer(connectionString));

        builder.Services.AddMediatR(typeof(Program));
        builder.Services.AddAutoMapper(typeof(Program));
        builder.Services.AddAllModules(typeof(Program));

        return services;
    }

    private static void AddAllModules(this IServiceCollection services, params Type[] types)
    {
       
        services.Scan(scan =>
            scan.FromAssembliesOf(types)
                .AddClasses(classes => classes.AssignableTo<IModule>())
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
    }
}