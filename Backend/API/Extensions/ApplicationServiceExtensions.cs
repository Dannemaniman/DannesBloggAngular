using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Data.Repositories;
using API.Helpers;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{
  public static class ApplicationServiceExtensions
  {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
      services.AddScoped<IAppService, AppService>();
      services.AddScoped<ITokenService, TokenService>();
      services.AddScoped<IUserRepository, UserRepository>();
      services.AddScoped<IThreadRepository, ThreadRepository>();
      //Sen måste vi säga åt den var profilesen ligger i vårt projekt.. Men vi har bara ett projekt.. 
      //så vi har bara en assembly av var dessa kan skapas.. det vi måste skapa på insidan.. är typeof.. och sedan identifiera AutoMapper profile classen vi skapat..:
      //Detta är nog för att AutoMappern ska gå och hitta dessa profiles.. som vi skapat inuti dessa klasserna! (CreateMap)!
      services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);
      //ORDNINGEN SPELAR INGEN ROLL HÄR... MEN DEN GÖR I CONFIGURE METODEN!
      services.AddDbContext<DataContext>(options =>
      {
        options.UseSqlite(config.GetConnectionString("DefaultConnection")); //Inuti appsettings.development.json så märker jag att, SQLites connection strings är jätteenkla.. bara namnet på filen där vi vill storea databasen..!
      });

      return services;
    }
  }
}