
using Catalog.Application.Base;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Catalog.Application.Repositories;

[Collection(nameof(UnityOfWorkTestFixture))]
public class UnityOfWorkTest
{

    readonly UnityOfWorkTestFixture _fixture;

    public UnityOfWorkTest(UnityOfWorkTestFixture fixture)
    => _fixture = fixture;

    [Fact(DisplayName = nameof(Commit))]
    [Trait("UnityOfWorkTest", "UnityOfWorkTest - Persistence")]
    public async Task Commit()
    {
        string id = Guid.NewGuid().ToString();
        var _dbContext = _fixture.CreateDBContext();
        var _exListCategory = _fixture.GetExCategoryList(10);
        await _dbContext.AddRangeAsync(_exListCategory);
        var _unityOfWork = new UnityOfWork(_dbContext);
        await _unityOfWork.Commit(CancellationToken.None);
        var assertDbContext = _fixture.CreateDBContext();
        var _categories = assertDbContext.Categories.AsNoTracking().ToList();
        _categories.Should().HaveCount(assertDbContext.Categories.ToList().Count);

    }
    
    [Fact(DisplayName = nameof(RollBack))]
    [Trait("UnityOfWorkTest", "UnityOfWorkTest - Persistence")]
    public async Task RollBack()
    {
        string id = Guid.NewGuid().ToString();
        var _dbContext = _fixture.CreateDBContext();
        var _unityOfWork = new UnityOfWork(_dbContext);

        var task = async () => await _unityOfWork.RollCack(CancellationToken.None);
        await task.Should().NotThrowAsync();
    }
}