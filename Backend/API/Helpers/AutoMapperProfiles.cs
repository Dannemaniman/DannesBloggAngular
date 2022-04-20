using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using AutoMapper;

namespace API.Helpers
{
  public class AutoMapperProfiles : Profile
  {
    //Vi skapar en automapper som ska mappa AppUser till MemberDto när vi retrievat en user från databasen...
    //Iden med AutoMapper.. är att den hjälper oss att mappa från ett objekt.. till ett annat.. Detta är det enda den gör!!
    public AutoMapperProfiles()
    {
      //.ForMember() BETYDER VILKEN PROPERTY VI VILL PÅVERKA!
      //SEDAN ÄR dest VILKEN PROPERTY VI VILL PÅVERKA
      //SEDAN SÄTTER VI OPTIONS. .MapFrom().. VAR VI VILL MAPPA FRÅN... : sektion 97
      CreateMap<AppUser, MemberDto>()
        .ForMember(dest => dest.Threads, opt => opt.MapFrom(src =>
          src.Threads.Any()));
      CreateMap<ThreadDto, UserThread>();
      CreateMap<MemberUpdateDto, AppUser>();
      CreateMap<UserThread, ReturnThread>();
    }
    //OM AUTOMAPPER TRÄFFAR EN METOD/PROPERTY MED NAMNET Get FÖRE.. SÅ KOMMER DEN KÖRA FUNKTIONEN OCH SÄTTA RETURNVÄRDET TILL AGE! IFALL BÄGGE ÄR INT!
    //Vi injectar denna som en dependency.. så vi lägger till den till application services extension!
  }
}

//Om det är enkelt.. är det inte oftast Optimalt.. så vi vill ställa oss frågan.. är det rätt att använda AutoMapper här..

//”Premature Optimization is the Root of all Evil!!” - Computer Science Quote
//Så optimization ska alltid komma senare?..
//Nej.. detta argumentet faller sönder, ifall där finns enkla optimeringar som jag kan lägga till snabbt utan att orsaka stora kodförändringar..