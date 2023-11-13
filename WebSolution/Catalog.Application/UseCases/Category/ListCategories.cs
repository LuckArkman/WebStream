using Catalog.Application.Common;
using Catalog.Application.Interfaces;
using Catalog.Domain.Repository;

namespace Catalog.Application.UseCases.Category
{
    public class ListCategories : IListCategories
    {
        private readonly ICategoryRepository _categoryRepository;

        public ListCategories(ICategoryRepository categoryRepository) 
            => _categoryRepository = categoryRepository;

        public async Task<ListCategoriesOutput> Handle(
            ListCategoriesInput request, 
            CancellationToken cancellationToken)
        {
            var searchOutput = await _categoryRepository.Search(
                new(
                    request.Page, 
                    request.PerPage, 
                    request.Search, 
                    request.Sort, 
                    request.Dir
                ),
                cancellationToken
            );

            return new ListCategoriesOutput(
                searchOutput.CurrentPage,
                searchOutput.PerPage,
                searchOutput.Total,
                searchOutput.Items
                    .Select(CategoryModelOutput.FromCategory)
                    .ToList()
            );
        }
    }
}