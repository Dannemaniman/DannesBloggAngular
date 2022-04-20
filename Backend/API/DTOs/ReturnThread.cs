using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class ReturnThread
    {
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? CategoryId { get; set; }
    public int? Replies { get; set; }
    public int? Views { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public DateTime WasCreated { get; set; }
    }
}
