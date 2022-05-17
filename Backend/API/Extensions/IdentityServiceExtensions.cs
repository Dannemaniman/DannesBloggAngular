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
        //The user lockout feature is enabled by default, but we state that here explicitly by setting 
        //the AllowedForNewUsers property to true. Additionally, we configure a lockout time span to two minutes 
        //(default is five) and maximum failed login attempts to three (default is five). Of course, the time span is set to 
        //two minutes just for the sake of this example, that value should be a bit higher in production environments.
        opt.Lockout.AllowedForNewUsers = true;
        opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        opt.Lockout.MaxFailedAccessAttempts = 3;
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

      services.AddAuthorization(opt =>
      {
        opt.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
        opt.AddPolicy("RequireDefaultRole", policy => policy.RequireRole("Admin", "Member"));
      });

      return services;
    }
  }
}