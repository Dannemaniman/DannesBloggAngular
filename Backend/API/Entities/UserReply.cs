namespace API.Entities
{
  public class UserReply
  {
    public int Id { get; set; }
    public DateTime WasCreated { get; set; } = DateTime.UtcNow;
    public DateTime WasModified { get; set; } = DateTime.UtcNow;
    public string? Title { get; set; }
    public string? Content { get; set; }
    public UserThread? Thread { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
  }
}