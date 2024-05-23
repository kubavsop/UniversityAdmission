using System.Security.Claims;
using Admission.AdminPanel.Models;
using Admission.Application.Common.Result;

namespace Admission.AdminPanel.Services;

public interface ICookieAuthService
{
    public Task<Result<ClaimsIdentity>> Login(LoginViewModel loginViewModel);
    public Task<Result<ClaimsIdentity>> Register(RegisterViewModel registerViewModel);
}