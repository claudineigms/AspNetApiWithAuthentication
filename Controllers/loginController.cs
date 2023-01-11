using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ApiWithAuthentication.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace ApiWithAuthentication.Controllers;

[ApiController]
[Route("[controller]")]
public class loginController : Controller
{
    private readonly UserManager<UserModel> _userManager;
    private readonly SignInManager<UserModel> _signInManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public loginController(UserManager<UserModel> userManager, 
        SignInManager<UserModel> signInManager,
        RoleManager<IdentityRole<int>> roleManager)
    {
        this._roleManager = roleManager;
        this._userManager = userManager;
        this._signInManager = signInManager;
    }

    [HttpGet("login")]
    public async Task<IActionResult> Login([FromQuery] UserLogin usuario)
    {
        var resultado = await _signInManager.PasswordSignInAsync(usuario.Login, usuario.Passworld, usuario.RememberLogin, false);
        Console.WriteLine(resultado);
        if (resultado.Succeeded)
        {
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("logout")]
    public async void Logout(){
        await _signInManager.SignOutAsync();
    }
}

