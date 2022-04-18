namespace API.Entities
{
  /*HAN KALLAR DEN FÖR AppUser.. PGA USER ÄR ETT VÄLDIGT ÖVERANVÄNT NAMN I KOD.. 
  SÅ AppUser VISAR OSS ATT DETTA ÄR VÅR USER .. OCH INTE EN USER SOM KOMMER FRÅN ANDRA DELAR AV dotnet RAMVERKET!*/
  public class AppUser
  {//skriv bara här 'prop' för att skapa en property
    public int Id { get; set; } //VI VILL ATT DEN SKA HETA Id.. SÅ ATT ENTITY FRAMEWORKET KÄNNER IGEN DEN SOM EN PRIMARY KEY!!
    /*
    OCH I MED ATT DET ÄR EN INTEGER SÅ KOMMER ENTITY FRAMEWORKET LÖSA SÅ ATT DATABASEN AUTO INCREMENTAR DEN VID VARJE NYTT RECORD!
    JAG KAN DOCK VÄLJA ANTINGEN ID MED UPPERCASE ELLER Id MED LOWERCASE I SLUTET!
    */
    public string? UserName { get; set; } //VI SKRIVER UserName MED STORT N PGA NÄR VI ANVÄNDER ASP.NET CORE IDENTITY.. ANNARS KOMMER VI BEHÖVA REFACTORA SENARE FÖR DET SKA FUNKA!!

    public byte[]? PasswordHash { get; set; }
    public byte[]? PasswordSalt { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? Age { get; set; }
    public string? Email { get; set; }
    public ICollection<UserThread>? Threads { get; set; }
    public DateTime? Created { get; set; } = DateTime.Now;
  }
}

//PUBLIC = KAN GETTAS OCH SETTAS I VARJE KLASS I APPEN
//PROTECTED = Propertyn kan accessas från antingen denna klassen.. ELLER KLASSER SOM INHERITAR FRÅN DENNA KLASSEN
//PRIVATE = denna propertyn kan bara accessas från denna klassen själv..:


//!!!!!!!!!!!!!!!!!!!!!!IMED ATT VI HAR ENTITY RAMVERKET.. SÅ MÅSTE ALLA DESSA PROPERTIES VARA PUBLIC SÅ DEN KAN SÄTTA DEM!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

/*
Entity Ramverket
 * Är en ORM 
 * Översätter vår kod till SQL commands som uppdaterar tables i vår databas!

Vi måste skapa en viktig class.. 
som “derives” från DbContext classen.. som vi får med Entity Frameworket.. 
denna klassen fungerar som en brygga.. mellan vår domän/entity classes.. och databasen..
DbContext classen är den primära vi använder för att interacta med vår databas..!

Det Entity Ramverket ger oss.. 
är att den tillåter oss att skriva “Link Queries” 
(DbContext.Users.Add(xxx)”

.Users REPRESENTERAR Users Tablet I VÅR DATABAS!.. OCH SEN ANVÄNDER VI METODEN FÖR ATT ADDA!


Entity Fungerar i samband med Database Providers!  Den vi ska använda för Development.. är SQLite !!
SQLite BETYDER ATT VI INTE BEHÖVER INSTALLERA EN DATABAS SERVER.. SQLITE ANVÄNDER BARA EN FIL FÖR ATT STOREA VÅR DATA!
SQLITE ÄR INTE!!!!! PRODUKTIONS VÄRDIGT!! MEN DET ÄR EN BRA DATABAS FÖR DEV.. FÖR DEN ÄR VÄLDIGT PORTABLE FÖR JAG BEHÖVER INTE INSTALLERA NGT!!
MEN FÖR DEV ÄR DETTA ETT BRA SÄTT ATT KOMMA IGÅNG SNABBT!!!

Vår SQLite Provider.. eller Database Provider.. är ansvarig för att översätta vår Link Query .. 
till ett SQL Command OCH EXECUTA DET!!! DbContext.Users.Add(kalle) -> SQLite Provider -> INSER INTO Users(Id, Name) Values(4, John)


--- ENTITY FRAMEWORK FEATURES ---
    * Querying - Gör något med databasen
    * Change Tracking - Håller koll på förändringar som sker!
    * Saving - Inser, Update, Delete
    * Concurrency  - Entity kör "Optimistic Concurrency" by default, Så operationer ska inte kunna krocka!
    * Transactions - Entity hanterar Transactions också! Och ger Automatic Transaction Management medans Querying och Saveing Data!
    * Caching - Den inkluderar Caching också! Så repeated Querying.. kommer returnera data från cachen! istället för att gå via Databasen!
    * Built-in-Conventions - Den ger oss också Built in Conventions (Id, och UserName t ex som vi gick igenom innan...)
    * Configurations - Vi kan också konfigurera våra Entities.. så vi kan skriva över default conventions:
    * Migrations - Tillåter oss att skapa en Databas Schema.. så när vi startar vår app med ett visst kommando.. kan vi automatiskt generera vår databas i vår databas server!!

*/