using MainWebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using MainWebApplication.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace MainWebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApplicationDbContext db;
        private UserManager<AspNetUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<AspNetUser> userManager)
        {
            db = context;
            _userManager = userManager;
        }
        private Task<AspNetUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        [AllowAnonymous]
        [HttpGet]
        public IActionResult RegisterOrganization()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public IActionResult RegisterOrganization(Organization organization)
        {
            organization.Id = organization.Number;
            db.Organization.Add(organization);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            /*ViewData["CheckUser"] = false;
            if (GetCurrentUserAsync().Result.FirstName != null || GetCurrentUserAsync().Result.LastName != null)
            {
            Проверка заполнено ли ФИО
                ViewData["CheckUser"] = true;
            }*/
            return View();
        }
        [Authorize(Roles = "Менеджер")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}