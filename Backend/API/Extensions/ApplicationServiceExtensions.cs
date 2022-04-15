using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
  public static class ApplicationServiceExtensions
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
      services.AddScoped<ITokenService, TokenService>();
      //ORDNINGEN SPELAR INGEN ROLL HÄR... MEN DEN GÖR I CONFIGURE METODEN!
      services.AddDbContext<DataContext>(options =>
      {
        options.UseSqlite(config.GetConnectionString("DefaultConnection")); //Inuti appsettings.development.json så märker jag att, SQLites connection strings är jätteenkla.. bara namnet på filen där vi vill storea databasen..!
      });

      return services;
    }
  }
}