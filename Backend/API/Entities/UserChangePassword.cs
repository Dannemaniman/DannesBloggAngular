using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    public class UserChangePassword
    {
        [Required, DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }
        
        [Required, DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage ="Confirm new password does not match")]
        public string ConfirmedPassword { get; set; }
    }
}