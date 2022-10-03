using Microsoft.AspNetCore.Mvc;
using MainWebApplication.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace MainWebApplication.Controllers
{
    [Authorize(Roles = "Администратор")]
    public class AdminController : Controller
    {
        ApplicationDbContext db;

        public AdminController(ApplicationDbContext context)
        {
            db = context;
        }
        public IActionResult Index()
        {
            var list = db.Organization;
            return View(list.ToList());
        }
    }
}