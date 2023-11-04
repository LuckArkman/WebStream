using System;
using Catalog.Domain.Exceptions;
using Catalog.Domain.SeedWork;

namespace Catalog.Domain.Entitys
{

    public class Category : AggregateRoot
    {
        public Category(string _name, string _description, bool isActive = true) : base()
        {
            Name = _name;
            Description = _description;
            IsActive = isActive;
            createTime = DateTime.Now;

            Validate();

        }

        void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new EntityValidationException($"{nameof(Name)} shold not be empty or null");
            if (Name.Length < 3) throw new EntityValidationException($"{nameof(Name)} shoud be lass 3 characters Long");
            if (Name.Length > 255)
                throw new EntityValidationException($"{nameof(Name)} shoud be lass or equal  255 characters Long");
            if (Description == null)
                throw new EntityValidationException($"{nameof(Description)} shold not be empty or null");
            if (Description.Length > 10000)
                throw new EntityValidationException(
                    $"{nameof(Description)} shoud be lass or equal  10.000 characters Long");
        }
        
        public string Name { get; set; }
        public string Description { get; set; }
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
        
        public void Update()
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