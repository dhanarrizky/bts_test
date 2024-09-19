using Microsoft.AspNetCore.Mvc;
using bts_test.Services;
using bts_test.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace bts_test.Controllers;

[ApiController]
[Route("/")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    // private readonly _service AuthServices;

    public AuthController(ILogger<AuthController> logger, AuthServices services)
    {
        _logger = logger;
        // _service = services;
    }

    [HttpPost("/login")]
    public IActionResult Login(LoginRequestModel req){
        return Ok();
    }

    [HttpPost("/register")]
    public IActionResult Register(LoginRequestModel req){
        return Ok();
    }
    
}
