using Microsoft.AspNetCore.Identity;

namespace MainWebApplication.Areas.Identity.Data
{
    public class RoleEdit
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<AspNetUser> Members { get; set; }
        public IEnumerable<AspNetUser> NonMembers { get; set; }
    }
}
