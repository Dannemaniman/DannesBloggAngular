using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs
{
    public class ReturnThread
    {
    public int? Id { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? CategoryId { get; set; }
    public int? RepliesCount { get; set; }
    public int? ViewsCount { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public DateTime? WasCreated { get; set; }
    public ICollection<UserReply>? Replies { get; set; }
    }
}
