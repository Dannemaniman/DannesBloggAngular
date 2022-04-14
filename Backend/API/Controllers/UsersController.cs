using System.Collections;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
/*
Viewn kommer komma från Angular! INNAN SÅ KOM Viewn FRÅN dotnet 
PGA I TIDIGARE VERSIONER LÅNGT TILLBAKA SKICKADE MAN TILLBAKA STATISKA SIDOR 
FRÅN WEBBSERVERN.. NU HAR VI EN FRONTEND APP.. SOM ÄR VIEWN!
*/

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
  private readonly DataContext _context;
  public UsersController(DataContext context)
  {
    _context = context;
  }

 [HttpGet]
    public ActionResult<IEnumerable<AppUser>> GetUsers()
    {
        var users = _context.Users?.ToList();

        return users;
    }

 [HttpGet("{Id}")]
    public ActionResult<AppUser> GetUserById(int id)
    {
        var user = _context.Users.Find(id);

        if(user == null) return NoContent();

        return user;
    }
}