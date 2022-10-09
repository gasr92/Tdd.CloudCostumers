using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Tdd.CloudCostumers.API.Config;
using Tdd.CloudCostumers.API.Models;
using Tdd.CloudCostumers.API.Services;
using Tdd.CloudCostumers.UnitTests.Fixtures;
using Tdd.CloudCostumers.UnitTests.Helpers;

namespace Tdd.CloudCostumers.UnitTests.Systems.Services
{
    public class TestUsersService
    {
        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokeHttpGetRequest()
        {
            //arrange
            var expectedResponse = UsersFixtures.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var endpoint = "https://example.com/users";
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });

            //sut = System Under Test
            var sut = new UserService(httpClient, config);

            //act
            await sut.GetAllUsers();


            //assert
            // verify if http request is made
            handlerMock
                .Protected()
                .Verify(
                    "SendAsync"
                    , Times.Exactly(1)
                    , ItExpr.Is<HttpRequestMessage>(r => r.Method == HttpMethod.Get)
                    ,ItExpr.IsAny<CancellationToken>()
            );
        }


        [Fact]
        public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
        {
            //arrange
            var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
            var httpClient = new HttpClient(handlerMock.Object);

            var endpoint = "https://example.com/users";
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });


            var sut = new UserService(httpClient, config);

            //act
            var result = await sut.GetAllUsers();

            //assert
            result.Count.Should().Be(0);
        }


        [Fact]
        public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
        {
            //arrange
            var expectedResponse = UsersFixtures.GetTestUsers();
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
            var httpClient = new HttpClient(handlerMock.Object);

            var endpoint = "https://example.com/users";
            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UserService(httpClient, config);

            //act
            var result = await sut.GetAllUsers();

            //assert
            result.Count.Should().Be(expectedResponse.Count);
        }


        [Fact]
        public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalURL()
        {
            //arrange
            var expectedResponse = UsersFixtures.GetTestUsers();

            var endpoint = "https://example.com/users";
            var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse, endpoint);
            var httpClient = new HttpClient(handlerMock.Object);

            var config = Options.Create(new UserApiOptions
            {
                Endpoint = endpoint
            });

            var sut = new UserService(httpClient, config);

            //act
            var result = await sut.GetAllUsers();

            //assert
            //result.Count.Should().Be(expectedResponse.Count);
            handlerMock
                .Protected()
                .Verify(
                    "SendAsync"
                    , Times.Exactly(1)
                    , ItExpr.Is<HttpRequestMessage>(r => 
                        r.Method == HttpMethod.Get
                        &&
                        r.RequestUri.ToString() == endpoint
                    )
                    , ItExpr.IsAny<CancellationToken>()
            );
        }
    }
}
