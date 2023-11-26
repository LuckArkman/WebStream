using Xunit;

namespace Catalog.Application.Repositories;

[CollectionDefinition(nameof(UnityOfWorkTestFixture))]
public class UnityOfWorkTestFixtureCollection : ICollectionFixture<UnityOfWorkTestFixture>{}