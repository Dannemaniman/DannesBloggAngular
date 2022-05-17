using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  public class AdminController : BaseApiController
  {
    private readonly UserManager<AppUser> _userManager;
    private readonly IBlockRepository _blockRepository;

    public AdminController(UserManager<AppUser> userManager, IBlockRepository blockRepository)
    {
      _blockRepository = blockRepository;
      _userManager = userManager;
    }


    [Authorize(Policy = "RequireAdminRole")]
    [HttpGet("users-with-roles")]
    public async Task<ActionResult> GetUsersWithRoles()
    {
      var users = await _userManager.Users
          .Include(r => r.UserRoles)
          .ThenInclude(r => r.Role)
          .OrderBy(u => u.UserName)
          .Select(u => new
          {
            u.Id,
            UserName = u.UserName,
            Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
          })
          .ToListAsync();

      return Ok(users);
    }


    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("edit-roles/{username}")]
    public async Task<ActionResult> EditRoles(string username, [FromQuery] string roles)
    {
      var selectedRoles = roles.Split(",").ToArray();

      var user = await _userManager.FindByNameAsync(username);

      var userRoles = await _userManager.GetRolesAsync(user);

      var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

      if (!result.Succeeded) return BadRequest();

      result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

      if (!result.Succeeded) return BadRequest();

      return Ok(await _userManager.GetRolesAsync(user));
    }


    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("block")]
    public async Task<ActionResult> BlockUser(BlockDto blockDto)
    {
      var converted = Int32.Parse(blockDto.Duration);

      var user = await _userManager.FindByNameAsync(blockDto.UserName);

      if (user == null) return BadRequest();

      DateTime currentDateMs = DateTime.UtcNow;
      var blockedUntil = currentDateMs.AddMilliseconds(converted * 1000);

      var blockedUser = new UserBlockList
      {
        UserId = user.Id,
        Duration = blockedUntil
      };

      var existingUser = await _blockRepository.IsUserBlocked(user.Id);

      if(existingUser == null) 
      {
      _blockRepository.Add(blockedUser);
      }

      if(existingUser != null)
      {
        existingUser.Duration = blockedUser.Duration;
        _blockRepository.Update(existingUser);
      }

      return Ok(await _blockRepository.SaveAllAsync());
    }


    [Authorize(Policy = "RequireAdminRole")]
    [HttpPost("unblock/{username}")]
    public async Task<ActionResult> UnblockUser(string username)
    {
      var user = await _userManager.FindByNameAsync(username);

      if (user == null) return BadRequest();

      var userBlockList = await _blockRepository.IsUserBlocked(user.Id);

      if(userBlockList == null) return NotFound();

      _blockRepository.DeleteBlock(userBlockList);

      if(await _blockRepository.SaveAllAsync()) return Ok();

      return BadRequest();
    }
  }
}