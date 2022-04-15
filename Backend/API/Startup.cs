using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace API
{
  public class Startup
  {
    //gillar inte användningen av "this.config = config", så byter namn till _config
    private readonly IConfiguration _config;
    public Startup(IConfiguration config)
    {
      _config = config;//vår configuration.. injectas in i min startup class (vår configuration):
    }

    // This method gets called by the runtime. Use this method to add services to the container. 
    //OM VI VILL GÖRA EN CLASS ELLER SERVICE AVAILABLE FÖR ANDRA DELAR AV VÅR APPLIKATION.. SÅ KAN VI ADDA DEM INUTI DENNA CONTAINER
    //OCH SÅ KOMMER DOTNET CORE LÖSA.. SKAPANDET AV DESSA CLASSER OCH DESTRUCTION AV DESSA CLASSER NÄR DEM INTE ANVÄNDS LÄNGRE!!
    public void ConfigureServices(IServiceCollection services)
    {
      //ORDNINGEN SPELAR INGEN ROLL HÄR... MEN DEN GÖR I CONFIGURE METODEN!
      services.AddDbContext<DataContext>(options =>
      {
        options.UseSqlite(_config.GetConnectionString("DefaultConnection")); //Inuti appsettings.development.json så märker jag att, SQLites connection strings är jätteenkla.. bara namnet på filen där vi vill storea databasen..!
      });
      services.AddScoped<ITokenService, TokenService>();
      services.AddControllers();
      services.AddCors();
      services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"])),
          ValidateIssuer = false, //Issuer av är vår API server.. 
          ValidateAudience = false, //Audience är vår angular app!
        };
      });
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      //VI KOLLAR FÖRST IFALL VI ÄR I DEV MODE.. OCH IFALL VI ÄR.. OCH VÅR APP FÅR ETT PROBLEM.. DÅ ANVÄNDER VI DEVELOPEREXCEPTION SIDAN!
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));
      }

      app.UseHttpsRedirection();//VI ANVÄNDER OCKSÅ HTTPSREDIRECTION.. SÅ IFALL USERN KOMMER IN VIA HTTPS.. SÅ REDIRECTAS USERN TILL HTTPS ENDPOINTS!!:

      app.UseRouting();

      app.UseCors(policy => policy.AllowAnyHeader().WithOrigins("http://localhost:4200"));

      app.UseAuthentication(); //måste vara under Cors och ovanför Authorization
      app.UseAuthorization();

      app.UseEndpoints(endpoints => //Och sen har vi middlewaret.. för att faktiskt använda mina endpoints.. och i den har vi en metod för att mappa mina controllers till dem!
      {
        endpoints.MapControllers();
      });
    }
  }
}
