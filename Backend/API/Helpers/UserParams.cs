namespace API.Helpers
{
    public class UserParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; }
        private int _pageSize = 10;
        //_pageSize KOMMER VARA VÅR DEFAULT PAGESIZE!..
        //Ifall clienten ger ett värde som inte är pageSize 10.. vill vi jämföra det med 
        //MaxPageSize.. så att den inte överskrider vad vi vill ge! Isåfall kommer vi sätta den till 50!

        //Vi lägger till en full property och skriver det såhär!!:
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
    }
}