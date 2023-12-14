using Catalog.Domain.SeedWork;
using Catalog.Domain.Validation;

namespace Catalog.Domain.Entitys
{

    public class Category : AggregateRoot
    {
        public Category(string? _name = null, string? _description = null, bool isActive = true) : base()
        {
            Name = _name ?? "";
            Description = _description;
            IsActive = isActive;
            createTime = DateTime.Now;

            Validate();

        }

        void Validate()
        {
            DomainValidation.NotNullOrEmpty(Name, nameof(Name));
            DomainValidation.MinLength(Name, 3, nameof(Name));
            DomainValidation.MaxLength(Name, 255, nameof(Name));
            DomainValidation.NotNull(Description, nameof(Description));
            DomainValidation.MaxLength(Description, 10000, nameof(Description));
        }
        
        public Category(string name, bool isActive, DateTime createTime) 
        {
            this.Name = name;
    this.IsActive = isActive;
    this.createTime = createTime;
   
        }
                public string Name { get; set; }
        public string? Description { get; set; }
        public bool IsActive { get; set; }

        public DateTime createTime { get; set; }

        public void Activate()
        {
            IsActive = true;
            Validate();
        }

        public void NotActivate()
        {
            IsActive = false;
            Validate();
        }

        public void Update(string _name, string? _description = null)
        {
            Name = _name;
            Description = _description ?? Description;
            
            Validate();
        }
    }
}