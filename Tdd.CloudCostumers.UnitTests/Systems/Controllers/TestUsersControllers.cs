using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Tdd.CloudCostumers.API.Controllers;
using Tdd.CloudCostumers.API.Models;
using Tdd.CloudCostumers.API.Services;
using Tdd.CloudCostumers.UnitTests.Fixtures;

namespace Tdd.CloudCostumers.UnitTests.Systems.Controllers;

public class TestUsersControllers
{
    [Fact]
    public async Task Get_OnSuccess_ReturnStatusCode200()
    {
        //arragne
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(x => x.GetAllUsers())
            .ReturnsAsync(UsersFixtures.GetTestUsers());
            //.ReturnsAsync(new List<User>()
            //{
            //    new()
            //    {
            //        Id = 1,
            //        Name = "Test",
            //        Email = "test@hotmail.com",
            //        Address = new Address{City="New York", Street="Madison", ZipCode = "123456"}
            //    }
            //});

        var sut = new UsersController(mockUserService.Object);

        //act
        var result = (ObjectResult) await sut.Get();


        //assert
        result.StatusCode.Should().Be(200);
    }

    [Fact]
    public async Task Get_OnSuccess_InvokesUserServiceExactlyOnce()
    {
        //arragne
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(s => s.GetAllUsers())
            .ReturnsAsync(new List<User>());

        var sut = new UsersController(mockUserService.Object);

        //act
        var result = await sut.Get();


        //assert
        //vai testar se ao chamar o metodo Get da controller, IUserServices sera chamado 1 vez
        mockUserService.Verify(s => s.GetAllUsers(), Times.Once());
    }

    [Fact]
    public async Task Get_OnSuccess_ReturnsListOfUsers()
    {
        //arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(s => s.GetAllUsers())
            .ReturnsAsync(UsersFixtures.GetTestUsers());
        
        var sut = new UsersController(mockUserService.Object);

        //act

        var result = await sut.Get();

        //assert
        result.Should().BeOfType<OkObjectResult>();

        var objectRes = (OkObjectResult)result;

        objectRes.Value.Should().BeOfType<List<User>>();
    }



    [Fact]
    public async Task Get_OnNonUsersFound_Returns404()
    {
        //arrange
        var mockUserService = new Mock<IUserService>();
        mockUserService
            .Setup(s => s.GetAllUsers())
            .ReturnsAsync(new List<User>());

        var sut = new UsersController(mockUserService.Object);

        //act
        var result = await sut.Get();

        //assert
        result.Should().BeOfType<NotFoundResult>();

        var objectRes = (NotFoundResult)result;
        objectRes.StatusCode.Should().Be(404);
    }
}