using System.Collections;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
/*
Viewn kommer komma från Angular! INNAN SÅ KOM Viewn FRÅN dotnet 
PGA I TIDIGARE VERSIONER LÅNGT TILLBAKA SKICKADE MAN TILLBAKA STATISKA SIDOR 
FRÅN WEBBSERVERN.. NU HAR VI EN FRONTEND APP.. SOM ÄR VIEWN!
*/

namespace API.Controllers;

public class UsersController : BaseApiController
{
  private readonly DataContext _context;
  public UsersController(DataContext context)
  {
    _context = context;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
  {
    return await _context.Users?.ToListAsync();

  }

  [HttpGet("{Id}")]
  public async Task<ActionResult<AppUser>> GetUser(int id)
  {
    var user = await _context.Users.FindAsync(id);

    if (user == null) return NoContent();

    return user;
  }
}