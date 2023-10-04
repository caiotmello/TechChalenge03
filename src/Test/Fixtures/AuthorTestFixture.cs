using Bogus;
using Domain.Models;

namespace Test.Fixtures
{
    public class AuthorTestFixture
    {
        private readonly Faker _faker;

        public AuthorTestFixture()
        {
            _faker = new Faker();
        }
        public Author GenerateAuthor()
        {
            var name = _faker.Random.String(20);
            var email = _faker.Internet.Email();

            return new(name, email);
        }

        public Author GenerateAuthorNameEmpty()
        {
            var name = string.Empty;
            var email = _faker.Internet.Email();

            return new(name, email);
        }

        public Author GenerateAuthorEmailEmpty()
        {
            var name = _faker.Random.String(20);
            var email = string.Empty;

            return new(name, email);
        }

        public Author GenerateAuthorNameMaxLenght()
        {
            var name = _faker.Random.String(70);
            var email = _faker.Internet.Email();

            return new(name, email);
        }
    }
}
