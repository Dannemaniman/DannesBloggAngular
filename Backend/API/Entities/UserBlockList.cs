namespace API.Entities
{
    public class UserBlockList
    {
        public int? Id { get; set; }
          public DateTime? Duration { get; set; }
          public AppUser? User { get; set; }
    }
}