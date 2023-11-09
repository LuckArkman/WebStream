using Catalog.Application.UseCases.Category;

namespace Catalog.Application.GetCategoryTest.Categorys;

public class GetCategoryOutPut
{
    public GetCategoryOutPut(Guid? id,string name, string description, bool isActive, DateTime createdAt)
    {
        Id = id ?? Guid.NewGuid();
        this.Name = name;
        Description = description ?? "";
        this.IsActive = isActive;
        this.createTime = createdAt;

    }
    
    public static GetCategoryOutPut FromCategory(Domain.Entitys.Category category)
    {
        return new (
            category.Id,
            category.Name,
            category.Description,
            category.IsActive,
            category.createTime
        );
    }
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime createTime { get; set; }
}