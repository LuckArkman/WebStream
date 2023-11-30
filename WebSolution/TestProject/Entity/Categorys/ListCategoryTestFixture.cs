using Catalog.Application.Interfaces;
using Catalog.Application.UseCases.Category;
using Catalog.Domain.Entitys;
using Catalog.Domain.Enum;
using Catalog.Domain.Repository;
using Moq;
using TestProject.Entity.Common;

namespace TestProject.Entity.Categorys
{
    public class ListCategoryTestFixture : BaseFixture
    {
        public Mock<ICategoryRepository> GetcategoryMock() => new Mock<ICategoryRepository>();
        public Mock<IunityOfWork> GetunityOfWorkMock() => new Mock<IunityOfWork>();

        public string GetValidCategoryName()
        {
            var categoryName = "";
            while(categoryName.Length < 3)
            {
                categoryName = faker.Commerce.Categories(1)[0];
            }
            if (categoryName.Length > 255)
            {
                categoryName = categoryName[..255];
            }
            return categoryName;
        }

        public string GetValidCategoryDescription(){
            var categoryDescription = faker.Commerce.ProductDescription();
            if(categoryDescription.Length > 10000)
            {
                categoryDescription = categoryDescription[..10000];
            }
            return categoryDescription;
        }

        public UpdateCategoryInput GetInvalidName()
        {
            var input = GetInput();
            while (input.Name.Length > 3)
            {
                input.Name = "12";

            }
            return input;
        }
        
        public UpdateCategoryInput GetInvalidLongName()
        {
            var input = GetInput();
            while (input.Name.Length < 255)
            {
                input.Name = $"{input.Name} {faker.Commerce.ProductName()}";
            }
            return input;
        }

        public UpdateCategoryInput GetInvalidLongDesc()
        {
            var input = GetInput();
            while (input.Description.Length < 10000)
            {
                input.Description = $"{input.Description} {faker.Commerce.ProductDescription()}";
            }
            return input;
        }

        public bool GetRamdomBool() => (new Random()).NextDouble() < 0.5;
        
        public Category GetValidCategory() => new (GetValidCategoryName(),GetValidCategoryDescription());
        
        public Category GetTestValidCategory() => new (GetValidCategoryName(),GetValidCategoryDescription(), GetRamdomBool());
        
        public List<Category> GetListValidCategory(int lenght = 10){
            var list = new List<Category>();
            for(int i = 0; i <lenght; i++)
            {
                list.Add(GetValidCategory());
            }
            return list;        
        }
        public UpdateCategoryInput GetValidInput(Guid? id = null)
            => new (
            id ?? Guid.NewGuid(), 
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            GetRamdomBool()
        );

        public UpdateCategoryInput GetInvalidShortName()
        {
            var input = GetInput();
            while (input.Name.Length > 3)
            {
                input.Name = "12";

            }
            return input;
        }
        public UpdateCategoryInput GetInvalidNullDesc()
        {
            var input = GetInput();
            while (input.Description != null)
            {
                input.Description = null;
            }
            return input;
        }
        
        public UpdateCategoryInput GetInput() => new(Guid.Empty,
            GetValidCategoryName(),
            GetValidCategoryDescription(),
            GetRamdomBool(),
            DateTime.Now
        );

        public ListCategoriesInput GetExampleInput()
        {
            var random = new Random();
            return new ListCategoriesInput(
                page: random.Next(1, 10),
                perPage: random.Next(15, 100),
                search: faker.Commerce.ProductName(),
                sort: faker.Commerce.ProductName(),
                dir: random.Next(0, 10) > 5 ?
                    SearchOrder.Asc : SearchOrder.Desc
            );
        }
    }
}