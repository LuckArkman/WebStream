namespace Catalog.Domain.Entity;

public class Category
{
    public Category(string _name, string _description, bool isActive = true)
    {
        Id = Guid.NewGuid();
        Name = _name;
        Description = _description;
        IsActive = isActive;
        createTime = DateTime.Now;

    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }

    public DateTime createTime { get; set; }
}