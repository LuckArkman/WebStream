namespace Catalog.Application.UseCases.Category;

public class CreateCategoryInput
{
    public CreateCategoryInput(Guid? id, string name, string? description = null, bool isActive = true, DateTime? createdAt = null)
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
}