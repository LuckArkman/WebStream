
using Catalog.Application.UseCases.Category;
using Catalog.Domain.Entitys;
using FluentValidation;

namespace Catalog.Domain.Validation
{
    public class UpdateCategoryInputValidator : AbstractValidator<UpdateCategoryInput>
    {
        public UpdateCategoryInputValidator()
        => RuleFor(x => x.Id).NotEmpty();
    }
}