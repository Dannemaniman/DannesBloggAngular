using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Controllers.Identity;
using API.Data;
using API.Data.Repositories;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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
    private readonly IThreadRepository _threadRepository;
    private readonly IReplyRepository _replyRepository;
    private readonly IBlockRepository _blockRepository;
    private readonly IMapper _mapper;

    public AccountController(
      IThreadRepository threadRepository, 
      IReplyRepository replyRepository, 
      RoleManager<AppRole> roleManager, 
      UserManager<AppUser> userManager, 
      SignInManager<AppUser> signInManager,
      IMapper mapper, 
      IBlockRepository blockRepository,
      ITokenService tokenService)
    {
      _replyRepository = replyRepository;
      _threadRepository = threadRepository;
      _roleManager = roleManager;
      _signInManager = signInManager;
      _userManager = userManager;
      _mapper = mapper;
      _tokenService = tokenService;
      _blockRepository = blockRepository;

    }

    [HttpGet("all-user-replies")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ReturnReply>>> getAllRepliesFromUser()
    {
      var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

      if (username == null) return BadRequest();

      var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == username);

      if (user == null)
      {
        return BadRequest();
      }

      if(await isUserBlocked(user))
      {
          _signInManager.SignOutAsync();
          return Unauthorized();
      }

      var replies = await _replyRepository.GetRepliesFromUserAsync(user);

      if (replies == null) return NotFound();

      var returnReplies = _mapper.Map<ReturnReply[]>(replies);

      return Ok(returnReplies);
    }

    [HttpGet("all-user-threads")]
    [Authorize]
    public async Task<ActionResult<IEnumerable<ReturnThread>>> getAllThreadsFromUser()
    {

      var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

      if (username == null) return BadRequest();

      var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == username);

      if (user == null)
      {
        return BadRequest();
      }

      if(await isUserBlocked(user))
      {
          _signInManager.SignOutAsync();
          return Unauthorized();
      }

      var threads = await _threadRepository.GetThreadsFromUserAsync(user);

      if (threads == null) return NotFound();

      var returnThread = _mapper.Map<ReturnThread[]>(threads);

      return Ok(returnThread);
    }

    [HttpPost("change-password")]
    [Authorize]
    public async Task<ActionResult> ChangePassword(UserChangePassword model)
    {
      if (ModelState.IsValid)
      {
        var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (username == null) return BadRequest();

        var user = await _userManager.Users.SingleOrDefaultAsync(x => x.UserName == username);

        if (user == null)
        {
          return BadRequest();
        }

        if(await isUserBlocked(user))
        {
          _signInManager.SignOutAsync();
          return Unauthorized();
        }

        var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

        if (!result.Succeeded)
        {
          return BadRequest();
        }

        // Upon successfully changing the password refresh sign-in cookie
        // await _signInManager.RefreshSignInAsync(user);
        return Ok();
      }

      return BadRequest();
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
        UserName = registerDto.UserName, 
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
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
      var user = await _userManager.Users
      .Include(t => t.Threads)
      .Include(r => r.Replies)
      .SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);

      if (user == null) return Unauthorized();

      if(await isUserBlocked(user))
      {
        return Unauthorized();
      }

      var result = await _signInManager
        .CheckPasswordSignInAsync(user, loginDto.Password, false);

      if (!result.Succeeded) return Unauthorized();

      return new UserDto
      {
        UserName = user.UserName,
        Token = await _tokenService.CreateToken(user)
      };
    }
    [Authorize]
    [HttpPost("logout")]
    public async Task Logout()
    {
      await _signInManager.SignOutAsync();
    }


    public async Task<bool> isUserBlocked(AppUser user) 
    {
      var userBlocked = await _blockRepository.IsUserBlocked(user.Id);

      if(userBlocked == null) return false;

      DateTime currentDateMs = DateTime.UtcNow;

      if(DateTime.Compare(currentDateMs, userBlocked.Duration) > 0)
      {
        this._blockRepository.DeleteBlock(userBlocked);
        return false;
      };

      return true;
    }



    private async Task<bool> UserExists(string username)
    {
      return await _userManager.Users.AnyAsync(x => x.UserName == username);
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