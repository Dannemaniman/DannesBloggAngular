using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class Category
    {
        [Required]
        public string? Code { get; set; }

        [Required]
        public string? Title { get; set; }
        
        [Required]
        public string? ThreadCount { get; set; }    
    }
}