using API.Data;
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
    [HttpPost("block-user/{username}/{duration}")]
    public async Task<ActionResult> BlockUser(string username, string duration)
    {
      var parsedDuration = DateTime.Parse(duration);

      if(parsedDuration == null) return BadRequest();

      var user = await _userManager.FindByNameAsync(username);

      if (user == null) return BadRequest();

      var blockedUser = _blockRepository.BlockUser(user, parsedDuration);

      return Ok(await _userManager.GetRolesAsync(user));
    }

    [Authorize(Policy = "ModeratePhotoRole")]
    [HttpGet("photos-to-moderate")]
    public ActionResult GetPhotosForModeration()
    {
      return Ok("Admins or Moderators can see this");
    }
  }
}