using MediatR;
using System.Reflection;

namespace catalog.API.Controllers;

public static class ControllersConfiguration
{
    public static IServiceCollection AddAndConfigureControllers(this IServiceCollection service)
    {
        service.AddMediatR(Assembly.GetExecutingAssembly());
        service.AddControllers();
        service.Explorer();
        return service;
    }

    static IServiceCollection Explorer(this IServiceCollection service)
    {
        service.AddEndpointsApiExplorer();
        service.AddSwaggerGen();
        return service;
    }

    public static WebApplication UseDocumentation(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        return app;
    }
}