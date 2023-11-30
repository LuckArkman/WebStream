using Castle.DynamicProxy;
using Catalog.Application.UseCases.Category;
using FluentValidation;

namespace Catalog.Application.GetCategoryTest.Categorys;

public class GetCategoryInputValidator : AbstractValidator<GetCategoryInput>
{
    public GetCategoryInputValidator()
        => RuleFor(x => x.Id).NotEmpty();
}