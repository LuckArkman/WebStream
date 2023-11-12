namespace Catalog.Application.Common;

public class CategoryModelOutput
{
    public CategoryModelOutput(Guid? id, string name, string? description = null, bool isActive = true, DateTime? createdAt = null)
    {
        Id = id ?? Guid.NewGuid();
        Name = name;
        Description = description ?? "";
        IsActive = isActive;
        createTime = createdAt ?? DateTime.Now;

    }
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime? createTime { get; set; }
    
    public static CategoryModelOutput FromCategory(Domain.Entitys.Category category)
        => new (
            category.Id,
            category.Name,
            category.Description,
            category.IsActive,
            category.createTime
        );
}