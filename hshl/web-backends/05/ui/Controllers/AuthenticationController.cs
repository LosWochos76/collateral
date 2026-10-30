using System.Security.Claims;
using Common.Models;
using Common.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoUI.Misc;

namespace ToDoUI.Models;

public class AuthenticationController : Controller
{
    private ILogger<AuthenticationController> logger;
    private IUserRepository userRepository;
    private EmailService mailService;
    private IPasswordHasher<User> passwordHasher;
    private IAuthenticationSchemeProvider schemeProvider;
    private IConfiguration config;

    public AuthenticationController(ILogger<AuthenticationController> logger, IUserRepository userRepository, EmailService mailService, 
        IPasswordHasher<User> passwordHasher, IAuthenticationSchemeProvider schemeProvider, IConfiguration config)
    {
        this.logger = logger;
        this.userRepository = userRepository;
        this.mailService = mailService;
        this.passwordHasher = passwordHasher;
        this.schemeProvider = schemeProvider;
        this.config = config;
    }

    [HttpGet("/Authentication/Login/")]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost("/Authentication/Authenticate/")]
    public async Task<IActionResult> Authenticate([FromForm] LoginViewModel login, [FromQuery] string? returnUrl)
    {
        if (!ModelState.IsValid)
            return View("Login", login);

        var user = userRepository.FindByLogin(login.EMail, login.Password);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "Invalid e-mail or password.");
            return View("Login", login);
        }

        var claimsIdentity = new ClaimsIdentity(user.ToClaims(),
            CookieAuthenticationDefaults.AuthenticationScheme, "email", "role");
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity));
        return LocalRedirect(returnUrl ?? "/");
    }

    [HttpPost("/Authentication/External/{provider}")]
    public async Task<IActionResult> External([FromRoute] string provider)
    {
        if (provider is not ("Google" or "Facebook") || await schemeProvider.GetSchemeAsync(provider) is null)
            return BadRequest();

        var props = new AuthenticationProperties {
            RedirectUri = "/Authentication/ExternalCallback" };
        props.Items["provider"] = provider;
        return Challenge(props, provider);
    }

    [HttpGet("/Authentication/ExternalCallback")]
    public async Task<IActionResult> ExternalCallback()
    {
        var result = await HttpContext.AuthenticateAsync("External");
        if (!result.Succeeded)
            return Redirect("/Authentication/Login");

        var provider = result.Properties!.Items["provider"]!;
        var subject = result.Principal!.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var email = result.Principal.FindFirstValue(ClaimTypes.Email);

        // the external account is identified by provider and subject, not by the e-mail-address
        var user = userRepository.FindByExternalLogin(provider, subject)
            ?? userRepository.AddExternalUser(provider, subject, email);

        await HttpContext.SignOutAsync("External");
        await HttpContext.SignInAsync(new ClaimsPrincipal(new ClaimsIdentity(user.ToClaims(),
            CookieAuthenticationDefaults.AuthenticationScheme, "email", "role")));
        return LocalRedirect("/");
    }

    [HttpPost("/Authentication/Logout/")]
    public async Task<IActionResult> Logout()
    {
        // with Keycloak, we also have to end the session at Keycloak
        if (config["Authentication:Mode"] == "Keycloak")
            return SignOut(new AuthenticationProperties { RedirectUri = "/" },
                CookieAuthenticationDefaults.AuthenticationScheme,
                OpenIdConnectDefaults.AuthenticationScheme);

        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return Redirect("/Authentication/Login");
    }

    [HttpGet("/Authentication/AccessDenied/")]
    public IActionResult AccessDenied()
    {
        return View();
    }

    [HttpGet("/Authentication/PasswordForgotten/")]
    public IActionResult PasswordForgotten()
    {
        return View();
    }

    [HttpPost("/Authentication/SendPasswordResetMail/")]
    public IActionResult SendPasswordResetMail([FromForm] PasswordForgottenViewModel pf)
    {
        if (!ModelState.IsValid)
            return View("PasswordForgotten", pf);

        // same answer, whether the user exists or not
        var user = userRepository.FindByEmail(pf.EMail);
        if (user is not null)
            mailService.SendPasswordResetMail(user);

        return View();
    }

    [HttpGet("/Authentication/ResetPassword/{token}")]
    public IActionResult ResetPassword([FromRoute] string token)
    {
        // finds only users with a valid token that has not expired yet
        var user = userRepository.FindByPasswordResetToken(token);
        if (user is null)
            return NotFound();

        return View(new PasswordResetViewModel() { Token = token});
    }

    [HttpPost("/Authentication/ResetPassword")]
    public IActionResult ResetPassword([FromForm] PasswordResetViewModel pr)
    {
        if (!ModelState.IsValid)
            return View("ResetPassword", pr);

        var user = userRepository.FindByPasswordResetToken(pr.Token);
        if (user is null)
        {
            ModelState.AddModelError(string.Empty, "The link is invalid or has expired.");
            return View("ResetPassword", pr);
        }

        user.PasswordHash = passwordHasher.HashPassword(user, pr.Password);
        user.PasswordResetToken = null;
        user.PasswordResetTokenExpires = null;
        userRepository.Update(user);
        return Redirect("/Authentication/Login");
    }

    [HttpGet("/Authentication/Register/")]
    public IActionResult Register()
    {
        return View();
    }
}
