using Tdd.CloudCostumers.API.Models;

namespace Tdd.CloudCostumers.UnitTests.Fixtures
{
    internal class UsersFixtures
    {
        public static List<User> GetTestUsers()
            =>
            new()
            {
                new User()
                {
                    Id = 1,
                    Name = "Test",
                    Email = "test@hotmail.com",
                    Address = new Address{City="New York", Street="Madison", ZipCode = "123456"}
                },
                new User()
                {
                    Id = 2,
                    Name = "Test 2",
                    Email = "test2@bol.com",
                    Address = new Address{City="Log Angeles", Street="Acacia 22", ZipCode = "2050"}
                },
                new User()
                {
                    Id = 2,
                    Name = "Last third user",
                    Email = "the3rd@bol.com",
                    Address = new Address{City="Paris", Street="No name", ZipCode = "87654321"}
                },
            };
    }
}
