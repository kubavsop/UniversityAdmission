using System.Security.Claims;
using Admission.AdminPanel.Models;
using Admission.AdminPanel.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.AdminPanel.Controllers;

public sealed class AccountController: Controller
{
    private readonly ICookieAuthService _cookieAuthService;
    
    public AccountController(ICookieAuthService cookieAuthService)
    {
        _cookieAuthService = cookieAuthService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View(new LoginViewModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginViewModel)
    {
        if (!ModelState.IsValid)
            return View(loginViewModel);
        var result = await _cookieAuthService.Login(loginViewModel);
        if (result.IsFailure)
        {
            ModelState.AddModelError("LoginErrors", result.Exception.Message);
            return View(loginViewModel);
        }

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(result.Value)
        );
        return RedirectToAction("Index", "home");
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View(new RegisterViewModel());
    }
    
    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
    {
        if (!ModelState.IsValid)
            return View(registerViewModel);
        var result = await _cookieAuthService.Register(registerViewModel);
        if (result.IsFailure)
        {
            ModelState.AddModelError("RegisterErrors", result.Exception.Message);
            return View(registerViewModel);
        }

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(result.Value)
        );
        return RedirectToAction("Index", "home");
    }
    
    [Authorize]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Account");
    }
}