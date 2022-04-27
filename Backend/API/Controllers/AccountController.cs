using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Controllers.Identity;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  public class AccountController : BaseApiController
  {
    private readonly ITokenService _tokenService;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly RoleManager<AppRole> _roleManager;
    public AccountController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
    {
      _roleManager = roleManager;
      _signInManager = signInManager;
      _userManager = userManager;
      _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
      if (await UserExists(registerDto.UserName)) return BadRequest("Username is taken.");

      var user = new AppUser
      {
        FirstName = registerDto.FirstName,
        LastName = registerDto.LastName,
        Age = registerDto.Age,
        Email = registerDto.Email,
        UserName = registerDto.UserName.ToLower(),
        Threads = null
      };

      var result = await _userManager.CreateAsync(user, registerDto.Password);

      if (!result.Succeeded) return BadRequest();

      var roleResult = await _userManager.AddToRoleAsync(user, "Member");

      if (!roleResult.Succeeded) return BadRequest();

      return new UserDto
      {
        UserName = user.UserName,
        Token = await _tokenService.CreateToken(user)
      };

      // await InitializeRoles();
      // var admin = await CreateAdmin();
      // return Ok(admin);
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
      var user = await _userManager.Users
      .Include(t => t.Threads)
      .Include(r => r.Replies)
      .SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);

      if (user == null) return BadRequest();

      var result = await _signInManager
        .CheckPasswordSignInAsync(user, loginDto.Password, false);

      if (!result.Succeeded) return BadRequest();

      var roleResult = await _userManager.AddToRoleAsync(user, "Member");

      if (!roleResult.Succeeded) return BadRequest();

      return new UserDto
      {
        UserName = user.UserName,
        Token = await _tokenService.CreateToken(user)
      };
    }

    private async Task<bool> UserExists(string username)
    {
      return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
    }

    private async Task InitializeRoles()
    {
      var roles = new List<AppRole>
      {
        new AppRole{Name = "Member"},
        new AppRole{Name = "Admin"},
      };

      foreach (var role in roles)
      {
        await _roleManager.CreateAsync(role);
      }
    }

    private async Task<UserDto> CreateAdmin()
    {
      var admin = new AppUser
      {
        UserName = "admin"
      };

      await _userManager.CreateAsync(admin, "Dannemaniman991!!");
      await _userManager.AddToRoleAsync(admin, "Admin");

      return new UserDto
      {
        UserName = admin.UserName,
        Token = await _tokenService.CreateToken(admin)
      };
    }
  }
}