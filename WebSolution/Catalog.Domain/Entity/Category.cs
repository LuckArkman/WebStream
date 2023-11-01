using Catalog.Domain.Exceptions;

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

        Validate();

    }

    void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name)) throw new EntityValidationException($"{nameof(Name)} shold not be empty or null");
        if (Description == null) throw new EntityValidationException($"{nameof(Description)} shold not be empty or null");
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }

    public DateTime createTime { get; set; }
}