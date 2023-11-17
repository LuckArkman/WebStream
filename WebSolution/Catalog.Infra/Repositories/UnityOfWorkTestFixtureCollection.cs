using Xunit;

namespace Catalog.Infra.Repositories;

[CollectionDefinition(nameof(UnityOfWorkTestFixture))]
public class UnityOfWorkTestFixtureCollection : ICollectionFixture<UnityOfWorkTestFixture>{}