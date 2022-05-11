using API.Entities;

namespace API.DTOs
{
    public class ReturnReply
    {
        public int Id { get; set; }
        public DateTime WasCreated { get; set; } = DateTime.UtcNow;
        public DateTime WasModified { get; set; } = DateTime.UtcNow;
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public ReturnThread? Thread { get; set; }
    }
}