using Bogus;

namespace TestProject.Entity.Common
{
    public abstract class BaseFixture
    {
        public BaseFixture() => faker = new Faker("pt_BR");

        public Faker faker { get; set; }
    }
}