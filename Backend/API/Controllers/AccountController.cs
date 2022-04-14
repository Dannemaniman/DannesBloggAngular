using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
  public class AccountController : BaseApiController
  {
    private readonly DataContext _context;
    public AccountController(DataContext context)
    {
      _context = context;
    }

    [HttpPost("register")]
    //En av sakerna som ApiController gör.. är att binda parameters den hittar till parametersen av min metod!
    //UTAN APICONTROLLER HADE VI BEHÖVT SKRIVA VART VI FÅR PARAMETRARNA IFRÅN ([FromBody])
    public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
    {
      //vi får tillgång till BadRequest pga ActionResult
      if (await UserExists(registerDto.UserName)) return BadRequest("Username is taken.");

      //using betyder att variabeln kommer förstöras efter statement blocket tar slut, eller:
      //"Using statement försäkrar oss att är vi är klar med HMAC classen..  så kommer den disposas av korrekt!"
      using var hmac = new HMACSHA512();

      var user = new AppUser
      {
        FirstName = registerDto.FirstName,
        LastName = registerDto.LastName,
        Age = registerDto.Age,
        Email = registerDto.Email,
        UserName = registerDto.UserName.ToLower(),
        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
        PasswordSalt = hmac.Key
      };

      _context.Users?.Add(user);

      await _context.SaveChangesAsync();

      return user;
    }

    private async Task<bool> UserExists(string username)
    {
      return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
    }
  }
}