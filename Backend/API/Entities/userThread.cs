using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
  public class UserThread
  {
    public int Id { get; set; }
    public int Replies { get; set; }
    public int Views { get; set; }
    public string? Content { get; set; }
    public string? Category { get; set; }

    //Här har vi en referens till Usern.. Entity KOMMER FÖRSÖKA RETURNERA EN APPUSER ALLTSÅ! .. OCH APPUSERN HAR EN Collection av Threads... FRAM OCH TILLBAKA PÅ DETTA SÄTTET... TILLS DET KRASCHAR!
    //(Circular dependency problem!)
    //LÖSNINGEN ÄR ATT VI FORMAR VÅR DATA INNAN VI RETURNERAR DEN!! MED EN DTO!!
    //EN ANNAN BRA ANLEDNING ATT ANVÄNDA DTOs.. ÄR ATT KOMMA OMKRING DETTA PROBLEMET!!
    public AppUser? User { get; set; }
    public DateTime? Created { get; set; } = DateTime.Now;
  }
}