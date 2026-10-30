using Common.Models;
using Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoService.Misc;

namespace ToDoService.Controllers;

[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly ILogger<AuthenticationController> logger;
    private readonly JwtTokenHelper jwtTokenHelper;
    private readonly IUserRepository userRepository;
    private readonly IConfiguration config;

    public AuthenticationController(
        ILogger<AuthenticationController> logger,
        JwtTokenHelper jwtTokenHelper,
        IUserRepository userRepository,
        IConfiguration config)
    {
        this.logger = logger;
        this.jwtTokenHelper = jwtTokenHelper;
        this.userRepository = userRepository;
        this.config = config;
    }

    [HttpPost("/Login")]
    [AllowAnonymous]
    public IActionResult Login([FromBody] LoginViewModel login)
    {
        // with Keycloak, tokens are issued by Keycloak and not by us
        if (config["Authentication:Mode"] == "Keycloak")
            return NotFound();

        var user = userRepository.FindByLogin(login.EMail, login.Password);
        if (user is null)
            return Unauthorized();  // do not tell, if e-mail or password was wrong

        var token = jwtTokenHelper.CreateToken(user);
        return Ok(new { access_token = token, token_type = "Bearer", expires_in = 3600 });
    }
}
