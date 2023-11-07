using System.Globalization;

namespace Catalog.Application.UseCases.Category;

public class CreateCategoryOutput
{
    public CreateCategoryOutput(Guid? id,string name, string description, bool isActive, DateTime createdAt)
    {
        Id = id ?? Guid.NewGuid();
        this.Name = name;
        Description = description ?? "";
        this.IsActive = isActive;
        this.createTime = createdAt;

    }
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public DateTime createTime { get; set; }

    public void ShouldNotBeNull()
    {
        
    }
}