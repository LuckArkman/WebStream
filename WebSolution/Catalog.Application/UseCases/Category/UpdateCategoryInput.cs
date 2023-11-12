using Catalog.Application.Common;
using Catalog.Application.Interfaces;
using Catalog.Domain.Repository;
using MediatR;

namespace Catalog.Application.UseCases.Category;

public class UpdateCategoryInput : IRequest<CategoryModelOutput>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool? IsActive { get; set; }
    public DateTime? createTime { get; set; }

    public UpdateCategoryInput(Guid id,string name, string? description = null, bool? isActive = null, DateTime? createTime = null)
    {
        Id = id;
        Name = name;
        Description = description;
        IsActive = isActive;
        this.createTime = createTime ?? DateTime.Now;
    }
}