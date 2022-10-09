using Microsoft.AspNetCore.Mvc;
using Tdd.CloudCostumers.API.Services;

namespace Tdd.CloudCostumers.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    //private readonly ILogger<UsersController> _logger;

    //public UsersController(ILogger<UsersController> logger)
    //{
    //    _logger = logger;
    //}

    private IUserService _userService { get; }

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpGet(Name = "GetUsers")]
    public async Task<IActionResult> Get()
    {
        var users = await _userService.GetAllUsers();
        //var users2 = await _userService.GetAllUsers();

        if(users.Any())
            return Ok(users);

        return NotFound();
    }
}
