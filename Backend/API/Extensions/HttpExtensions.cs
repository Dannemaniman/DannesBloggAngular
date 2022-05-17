using System.Text.Json;
using API.Helpers;

namespace API.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader(this HttpResponse response, 
        int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            //GLÖM INTE ATT DET MÅSTE VARA I RÄTT ORDNING!!
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            //. Innan när man använde custom headers (vilket detta är) Så la man till ett x innan! Så X-Pagination... 
            //Men det gör vi inte längre... ENDA REQUIRET ÄR ATT GE HEADERN ETT SENSIBLE NAMN!:
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader));
            //SEDAN, MÅSTE VI LÄGGA TILL EN CORS-HEADER, PGA VI SKAPAR EN CUSTOM HEADER!
            //DETTA GÖR VI FÖR ATT GÖRA VÅR CUSTOM-HEADER AVAILABLE!
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
            //Sedan måste vi skapa en class så vi kan få tag på Pagination headern från usern!!
            //Vi kommer ta alla parametrarna som en query string! Och vi kommer förvara dem i ett objekt!
            //Detta objektet heter UserParams
        }
    }
}