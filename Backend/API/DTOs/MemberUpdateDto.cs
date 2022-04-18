using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class MemberUpdateDto
    {
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? NewPassword { get; set; }
    }
}