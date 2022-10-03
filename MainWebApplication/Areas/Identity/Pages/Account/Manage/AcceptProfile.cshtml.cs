using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MainWebApplication.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MainWebApplication.Areas.Identity.Pages.Account.Manage
{
    public class AcceptProfileModel : PageModel
    {
        ApplicationDbContext db;
        private UserManager<AspNetUser> _userManager;
        public AcceptProfileModel(ApplicationDbContext context, UserManager<AspNetUser> userManager)
        {
            db = context;
            _userManager = userManager;
        }
        [BindProperty]
        public InputModel Input { get; set; }
        public class InputModel : Organization
        {
            public string StatusMessage { get; set; }
        }
        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }
        private Task<AspNetUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public async Task<IActionResult> OnPostAsync()
        {
            IdentityResult result;
            AspNetUser user = await _userManager.FindByIdAsync(GetCurrentUserAsync().Result.Id);
            if (Input.Password == db.Organization.Where(x => x.Id == GetCurrentUserAsync().Result.OrganizationId).Select(x => x.Password).FirstOrDefault())
            {
                result = await _userManager.AddToRoleAsync(user, "Руководитель");
                if (!result.Succeeded)
                    Errors(result);
            }
            return Page();
        }
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }

    }
}
