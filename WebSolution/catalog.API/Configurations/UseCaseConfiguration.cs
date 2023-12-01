using Catalog.Application.Base;
using Catalog.Application.Repositories;
using Catalog.Application.UseCases.Category;
using MediatR;

namespace catalog.API.Configurations;

public static class UseCaseConfiguration
{
    public static IServiceCollection AddUseCase(this IServiceCollection service)
    {
        service.AddMediatR(typeof(CreateCategory));
        service.AddRepository();
        return service;
    }

    public static IServiceCollection AddRepository(this IServiceCollection service)
    {
        service.AddTransient<ICategoryRepository, CategoryRepository>();
        service.AddTransient<IUnityOfWork, UnityOfWork>();
        return service;
    }
}