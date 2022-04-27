using API.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Controllers.Identity
{
    public class AppRole : IdentityRole<int>
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}