using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
  public class UserThread
  {
    public int Id { get; set; }
    public DateTime WasCreated { get; set; } = DateTime.UtcNow;
    public DateTime WasModified { get; set; } = DateTime.UtcNow;
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? CategoryId { get; set; }
    public int RepliesCount { get; set; } = 0;
    public int ViewsCount { get; set; } = 0;
    public ICollection<UserReply>? Replies { get; set; }
    public AppUser? User { get; set; }
  }
}

    //Här har vi en referens till Usern.. 
    //Entity KOMMER FÖRSÖKA RETURNERA EN APPUSER ALLTSÅ! .. 
    //OCH APPUSERN HAR EN Collection av Threads... 
    //FRAM OCH TILLBAKA PÅ DETTA SÄTTET... TILLS DET KRASCHAR!
    //(Circular dependency problem!)
    //LÖSNINGEN ÄR ATT VI FORMAR VÅR DATA INNAN VI RETURNERAR DEN!! MED EN DTO!!
    //EN ANNAN BRA ANLEDNING ATT ANVÄNDA DTOs.. 
    //ÄR ATT KOMMA OMKRING DETTA PROBLEMET!!