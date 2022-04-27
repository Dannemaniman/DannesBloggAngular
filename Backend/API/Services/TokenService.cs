using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
  public class TokenService : ITokenService
  {
    private readonly SymmetricSecurityKey _key;
    private readonly UserManager<AppUser> _userManager;
    public TokenService(IConfiguration config, UserManager<AppUser> userManager)
    {
      _userManager = userManager;
      _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));

    }

    public async Task<string> CreateToken(AppUser user)
    {
      //Dessa har vi för tillfället.. och token är ett säkert ställa att lägga Userns role.. Usern kan inte 
      //modifiera token och injecta fel Role.. SÅVIDA DEM INTE HAR SECRET KEY SOM JAG ANVÄNDER PÅ SERVERN!!:
      var claims = new List<Claim>
      {
          new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
      };

      var roles = await _userManager.GetRolesAsync(user);
      //Sedan lägger vi till roles med .AddRange.. och vi Projectar därifrån med Select!
      //Vi selectar roles från en list av roles.. där vi sedan skapar en ny claim.. och 
      //istället för att använda JwtRegisterdClaimNames, använder vi ClaimTypes.Role
      //DETTA PGA JwtRegisterdClaimsTypes HAR INTE EN CLAIMTYPE FÖR ROLES!
      claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

      var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.Now.AddDays(7),
        SigningCredentials = creds
      };

      var tokenHandler = new JwtSecurityTokenHandler();

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return tokenHandler.WriteToken(token);
    }
  }
}