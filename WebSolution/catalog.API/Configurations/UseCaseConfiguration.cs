using Catalog.Application.Interfaces;
using Catalog.Application.UseCases.Category;
using Catalog.Domain.Repository;
using Catalog.Infra.Base;
using Catalog.Infra.Repositories;
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
        service.AddTransient<IunityOfWork, UnityOfWork>();
        return service;
    }
}