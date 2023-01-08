﻿using Auth0.AspNetCore.Authentication;
using DotNetNinja.QrCodes.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNetNinja.QrCodes.Controllers;
public class AccountController : MvcController
{
    private readonly IWebHostEnvironment _environment;
    private readonly ISignInService _signInService;

    public AccountController(ILogger<AccountController> logger, IWebHostEnvironment environment, ISignInService signInService) : base(logger)
    {
        _environment = environment;
        _signInService = signInService;
    }

    [HttpGet, AllowAnonymous]
    public ViewResult AccessDenied()
    {
        return View();
    }

    [HttpGet, AllowAnonymous]
    public async Task LogIn(string returnUrl = "/")
    {
        var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
            .WithRedirectUri(returnUrl)
            .Build();

        await _signInService.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
    }
    
    [HttpGet]
    public IActionResult Diagnostics()
    {
        if (!_environment.IsDevelopment())
        {
            return NotFound();
        }
        return View(User.Claims);
    }

    [HttpGet]
    public async Task LogOut()
    {
        var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
            .WithRedirectUri("/")
            .Build();

        await _signInService.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        await _signInService.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

}
