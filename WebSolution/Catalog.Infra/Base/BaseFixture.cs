using Bogus;

namespace Catalog.Infra.Base
{
    public class BaseFixture
    {
        public BaseFixture() => faker = new Faker("pt_BR");

        protected Faker faker { get; set; }

    }
}