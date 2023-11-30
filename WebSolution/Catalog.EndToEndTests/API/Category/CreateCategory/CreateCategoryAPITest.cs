using System.Net;
using Catalog.Application.Common;
using Catalog.Application.UseCases.Category;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace Catalog.EndToEndTests.API.Category.CreateCategory
{

    [Collection(nameof(CreateCategoryAPITestFixture))]
    public class CreateCategoryApiTest : IDisposable
    {
        private readonly CreateCategoryAPITestFixture _fixture;

        public CreateCategoryApiTest(CreateCategoryAPITestFixture fixture)
            => _fixture = fixture;

        [Fact(DisplayName = nameof(CreateCategory))]
        [Trait("CreateCategoryApiTest", "CreateCategoryApiTest - Endpoints")]
        public async Task CreateCategory()
        {
            var input = _fixture.getExampleInput();

            var (response,output) = await _fixture.ApiClient.Post<CategoryModelOutput>(
                "/categorys",input);

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.Created);
            output.Should().NotBeNull();
            output!.Name.Should().Be(input.Name);
            output.Description.Should().Be(input.Description);
            output.IsActive.Should().Be(input.IsActive);
            output.Id.Should().NotBeEmpty();
            output.CreatedAt.Should().NotBeSameDateAs(default);
            var dbCategory = await _fixture.Persistence.GetById(output.Id);
            dbCategory.Should().NotBeNull();
            dbCategory!.Name.Should().Be(input.Name);
            dbCategory.Description.Should().Be(input.Description);
            dbCategory.IsActive.Should().Be(input.IsActive);
            dbCategory.Id.Should().NotBeEmpty();
            dbCategory.createTime.Should().NotBeSameDateAs(default);
        }
        
        [Xunit.Theory(DisplayName = nameof(ErrorWhenCantInstantiate))]
        [Trait("CreateCategoryApiTest", "CreateCategoryApiTest - Endpoints")]
        [MemberData(
            nameof(CreateCategoryAPITestDataGenerator.GetInvalidInputs),
            MemberType = typeof(CreateCategoryAPITestDataGenerator)
        )]
        public async Task ErrorWhenCantInstantiate(
            CreateCategoryInput input,
            string expectedDetail
        ){
            var (response, output) = await _fixture.
                ApiClient.Post<ProblemDetails>(
                    "/categories",
                    input
                );

            response.Should().NotBeNull();
            response!.StatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
            output.Should().NotBeNull();
            output!.Title.Should().Be("One or more validation errors ocurred");
            output.Type.Should().Be("UnprocessableEntity");
            output.Status.Should().Be((int)StatusCodes.Status422UnprocessableEntity);
            output.Detail.Should().Be(expectedDetail);
        }

        public void Dispose()
            => _fixture.CleanPersistence();
    }
}