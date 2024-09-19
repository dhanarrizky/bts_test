using Microsoft.AspNetCore.Mvc;
using bts_test.Services;
using bts_test.Models;

namespace bts_test.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly ILogger<AuthController> _logger;
    private readonly _service AuthServices;

    public AuthController(ILogger<AuthController> logger, AuthServices services)
    {
        _logger = logger;
        _service = services;
    }

    [HttpPost]
    public IActionResult Login(LoginRequestModel req){
        
    }
    
}
