using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HolboxOne.Shared.DTOs;
using HolboxOne.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HolboxOne.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public AccountsController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    /// <summary>
    /// Registro de usuario Cliente (desde React)
    /// </summary>
    [HttpPost("register/client")]
    public async Task<ActionResult<TokenDTO>> RegisterClient(RegisterClientDTO model)
    {
        var user = new User
        {
            FullName = model.FullName,
            Email = model.Email,
            UserName = model.Email,
            UserType = UserType.Client
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return BuildToken(user);
    }

    /// <summary>
    /// Registro de usuario Administrador (desde Blazor, solo por otros admins)
    /// </summary>
    [HttpPost("register/admin")]
    // TODO: Agregar [Authorize(Roles = "Admin")] cuando se implementen roles
    public async Task<ActionResult<TokenDTO>> RegisterAdmin(RegisterAdminDTO model)
    {
        var user = new User
        {
            FullName = model.FullName,
            Email = model.Email,
            UserName = model.Email,
            UserType = UserType.Admin
        };

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return BuildToken(user);
    }

    /// <summary>
    /// Login con Email y Contraseña
    /// </summary>
    [HttpPost("login")]
    public async Task<ActionResult<TokenDTO>> Login(LoginDTO model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            return Unauthorized("Email o contraseña incorrectos");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);

        if (!result.Succeeded)
        {
            return Unauthorized("Email o contraseña incorrectos");
        }

        return BuildToken(user);
    }

    /// <summary>
    /// Obtener información del usuario actual
    /// </summary>
    [HttpGet("me")]
    public async Task<ActionResult<UserDTO>> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        if (email == null)
        {
            return Unauthorized();
        }

        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return NotFound();
        }

        return new UserDTO
        {
            Id = user.Id,
            FullName = user.FullName,
            Email = user.Email!,
            UserType = user.UserType.ToString(),
            ProfilePicture = user.ProfilePicture
        };
    }

    private TokenDTO BuildToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.FullName),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim("UserType", user.UserType.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["jwtKey"]!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expiration = DateTime.UtcNow.AddDays(30);

        var token = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims: claims,
            expires: expiration,
            signingCredentials: credentials);

        return new TokenDTO
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            Expiration = expiration
        };
    }
}
