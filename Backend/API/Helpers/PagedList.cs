using Microsoft.EntityFrameworkCore;

namespace API.Helpers
{
    public class PagedList<T> : List<T>
    {
    public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        CurrentPage = pageNumber;
        TotalPages = (int) Math.Ceiling(count / (double) pageSize);
        PageSize = pageSize;
        TotalCount = count;
        //Vi vill också lägga till Range av itemsen inuti denna constructorn! Nu har vi access till dessa itemsen inuti vår pageList!!
        AddRange(items);
    }

    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        //DETTA KOMMER GÖRA ETT DATABASE CALL... DETTA ÄR UNAVOIDABLE.. MEN VI MÅSTE ANVÄNDA DENNA COUNTEN!
        //PGA DET VI FÅR TILLBAKA FRÅN VÅR DATABAS NÄR VI KÖR VÅR RIKTIGA QUERY.. KOMMER INTE VARA SAMMA SOM ANTALET AVAILABLE RECORDS ÄR...
        //Där var första queryn iallafall!
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();    
        return new PagedList<T>(items, count, pageNumber, pageSize);
        //DETTA SER KONSTIGT UT FÖR EN KLASS.. PGA JAG HAR EN STATISK METOD SOM SEDAN, EFFECTIVELY, SKAPAR EN NY INSTANCE DENNA KLASSEN SOM VI SEDAN RETURNAR!  SOM INNEHÅLLER ALLA PROPERTIES!
        //DET ÄR DETTA SOM VI GÖR MED AddRange().. VI VILL ADDA ALLA DESSA PROPERTIES TILL DENNA KLASSEN.. SÅ ATT NÄR VI SKAPAR EN NY INSTANCE AV DENNA KLASSEN..
        //VILKET VI GÖR MED CreateAsync METODEN!..
        //VI RÄKNAR UT COUNT OCH ITEMS OCH FRÅN DENNA METODEN SÅ SKAPAR VI EN NY INSTANCE AV KLASSEN:

        //!!! VIKTIGT
        //  ANLEDNINGEN AddRange() ANVÄNDS.. ÄR FÖR ATT.. DET ÄR DET SOM KOMMER VARA DEN FAKTISKA LISTAN AV ITEMS SOM VI SKICKAR TILL FRONTEND APPEN! OCH AddRange() KOMMER LÄGGA TILL DET PÅ SLUTET AV Class OBJEKTET NÄR VI INSTANCERAR!
        //  SÅ AddRange() ANVÄNDS FÖR ATT LÄGGA TILL ITEMS PÅ OBJEKTET SOM INSTANCERAS!
        //!!! VIKTIGT
    }

    }
}