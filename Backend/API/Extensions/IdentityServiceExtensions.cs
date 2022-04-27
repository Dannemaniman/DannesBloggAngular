using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Controllers.Identity;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace API.Extensions
{
  public static class IdentityServiceExtensions
  {
    public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
    {

      services.AddIdentityCore<AppUser>(opt =>
      {
        opt.Password.RequireNonAlphanumeric = false; //Ifall jag vill förenkla lösenordet
        //opt.SignIn. KAN OCKSÅ ANVÄNDAS FÖR ATT FÖRENKLA LOGIN
      })
        .AddRoles<AppRole>()
        .AddRoleManager<RoleManager<AppRole>>()
        .AddSignInManager<SignInManager<AppUser>>()
        .AddRoleValidator<RoleValidator<AppRole>>()
        .AddEntityFrameworkStores<DataContext>();

      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
          options.TokenValidationParameters = new TokenValidationParameters
          {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"])),
            ValidateIssuer = false, //Issuer av är vår API server.. 
            ValidateAudience = false, //Audience är vår angular app!
          };
        });

      return services;
    }
  }
}