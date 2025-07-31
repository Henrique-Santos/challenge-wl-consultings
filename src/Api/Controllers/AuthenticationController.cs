using Api.Contracts;
using Api.Extensions;
using Api.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Api.Controllers;

public class AuthenticationController : ApiController
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IJwtService _jwtService;
    private readonly JwtIdentity _jwtIdentity;

    public AuthenticationController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IJwtService jwtService, IOptions<JwtIdentity> jwtIdentity)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtService = jwtService;
        _jwtIdentity = jwtIdentity.Value;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> Register(RegisterRequest registerViewModel)
    {
        if (!ModelState.IsValid)
        {
            return ReportError(ModelState);
        }

        var user = new IdentityUser
        {
            UserName = registerViewModel.UserName,
            Email = registerViewModel.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, registerViewModel.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return ReportError(ModelState);
        }

        await _signInManager.SignInAsync(user, false);

        var response = new RegisterResponse
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email,
            AccessToken = _jwtService.GenerateJwtToken(),
            ExpiresIn = _jwtIdentity.ExpirationInHours * 3600
        };

        return ReportSuccess(response);
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> Login(LoginRequest loginViewModel)
    {
        if (!ModelState.IsValid)
        {
            return ReportError(ModelState);
        }

        var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, true);

        if (result.IsLockedOut)
        {
            ModelState.AddModelError(string.Empty, "User is locked out.");
            return ReportError(ModelState);
        }

        if (!result.Succeeded)
        {
            ModelState.AddModelError(string.Empty, "Invalid credentials.");
            return ReportError(ModelState);
        }

        var response = new LoginResponse
        {
            AccessToken = _jwtService.GenerateJwtToken(),
            ExpiresIn = _jwtIdentity.ExpirationInHours * 3600
        };

        return ReportSuccess(response);            
    }
}