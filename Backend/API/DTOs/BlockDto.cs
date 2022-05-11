using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class BlockDto
    {
        [Required]
        public string? UserName { get; set; }

        [Required]
        public string? Duration { get; set; }
    }
}