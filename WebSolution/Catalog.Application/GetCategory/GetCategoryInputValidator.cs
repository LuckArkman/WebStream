using Castle.DynamicProxy;
using FluentValidation;

namespace Catalog.Application.GetCategoryTest.Categorys;

public class GetCategoryInputValidator : AbstractValidator<GetCategoryInput>
{
    public GetCategoryInputValidator()
        => RuleFor(x => x.Id).NotEmpty();
}