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
    private readonly AuthServices _service;

    public AuthController(ILogger<AuthController> logger, AuthServices services)
    {
        _logger = logger;
        _service = services;
    }

    [HttpPost("/login")]
    public IActionResult Login(LoginRequestModel req){
        var res = _service.Login(req);
        return Ok(res);
    }

    [HttpPost("/register")]
    public IActionResult Register(RegistrationRequestModel req){
        try {
            _service.Register(req);
            return Ok();
        } catch {
            return StatusCode(StatusCodes.Status500InternalServerError, "Error, It's just for example");
        }
    }
    
}
