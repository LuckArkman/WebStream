namespace Catalog.Domain.Entity;

public class Category
{
    public Category(string _name, string _description)
    {
        Name = _name;
        Description = _description;

    }
    public string Name { get; set; }
    public string Description { get; set; }
}