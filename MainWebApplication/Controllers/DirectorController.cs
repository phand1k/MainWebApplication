using Microsoft.AspNetCore.Mvc;
using MainWebApplication.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MainWebApplication.Models;
using System.ComponentModel.DataAnnotations;

namespace MainWebApplication.Controllers
{
    [Authorize(Roles = "Руководитель")]
    public class DirectorController : Controller
    {
        private UserManager<AspNetUser> userManager;
        private RoleManager<IdentityRole> roleManager;
        private UserManager<AspNetUser> _userManager;
        ApplicationDbContext db;
        public DirectorController(RoleManager<IdentityRole> roleMgr, UserManager<AspNetUser> userMrg, ApplicationDbContext context)
        {
            roleManager = roleMgr;
            userManager = userMrg;
            db = context;
            _userManager = userManager;
        }
        public ViewResult Index() => View(roleManager.Roles);
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            else
                ModelState.AddModelError("", "No role found");
            return View("Index", roleManager.Roles);
        }
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required] string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                    return RedirectToAction("Index");
                else
                    Errors(result);
            }
            return View(name);
        }


        // other methods

        public async Task<IActionResult> Update(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<AspNetUser> members = new List<AspNetUser>();
            List<AspNetUser> nonMembers = new List<AspNetUser>();
            foreach (AspNetUser user in userManager.Users)
            {
                var list = await userManager.IsInRoleAsync(user, role.Name) ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEdit
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers
            });
        }
        [HttpPost]
        public async Task<IActionResult> Update(RoleModification model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                foreach (string userId in model.AddIds ?? new string[] { })
                {
                    AspNetUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
                foreach (string userId in model.DeleteIds ?? new string[] { })
                {
                    AspNetUser user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user, model.RoleName);
                        if (!result.Succeeded)
                            Errors(result);
                    }
                }
            }

            if (ModelState.IsValid)
                return RedirectToAction(nameof(Index));
            else
                return await Update(model.RoleId);
        }
        private void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
        private Task<AspNetUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public IActionResult CreateSpecialist()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateSpecialist(Specialist specialist)
        {
            var checkSpecialist = db.Specialists.Where(x=>x.OrganizationId == GetCurrentUserAsync().Result.OrganizationId)
                .FirstOrDefault(x=>x.Name == specialist.Name);
            if (checkSpecialist == null)
            {
                specialist.OrganizationId = GetCurrentUserAsync().Result.OrganizationId;
                db.Specialists.Add(specialist);
                db.SaveChanges();
                return RedirectToAction("SpecialistsList");
            }
            ModelState.AddModelError("Name", "Такой специалист уже существует");
            return View(specialist);
        }
        public IActionResult SpecialistsList()
        {
            var listSpecialist = db.Specialists.Where(x=>x.OrganizationId == GetCurrentUserAsync().Result.OrganizationId);
            return View(listSpecialist.ToList());
        }
        public IActionResult CreateAnalysis()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAnalysis(Analysis analysis)
        {
            var checkService = db.Analyses.Where(x => x.OrganizationId == GetCurrentUserAsync().Result.OrganizationId).FirstOrDefault(x => x.Name == analysis.Name);
            if (checkService == null)
            {
                analysis.OrganizationId = GetCurrentUserAsync().Result.OrganizationId;
                db.Analyses.Add(analysis);
                db.SaveChanges();
                return RedirectToAction("ListAnalyses");
            }
            ModelState.AddModelError("Name", "Такой анализ уже существует");
            return View(analysis);
        }
        public IActionResult ListServices()
        {
            var listServices = db.Services.Where(x=>x.OrganizationId == GetCurrentUserAsync().Result.OrganizationId);
            return View(listServices.ToList());
        }
        public IActionResult ListAnalyses()
        {
            var listAnalyses = db.Analyses.Where(x => x.OrganizationId == GetCurrentUserAsync().Result.OrganizationId);
            return View(listAnalyses.ToList());
        }
        public IActionResult CreateService()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateService(Service service)
        {
            var checkService = db.Services.Where(x=>x.OrganizationId == GetCurrentUserAsync().Result.OrganizationId).FirstOrDefault(x=>x.Name == service.Name);
            if (checkService == null)
            {
                service.OrganizationId = GetCurrentUserAsync().Result.OrganizationId;
                db.Services.Add(service);
                db.SaveChanges();
                return RedirectToAction("ListServices");
            }
            ModelState.AddModelError("Name", "Такая услуга уже существует");
            return View(service);
        }
        public IActionResult ListUsers()
        {
            var usersList = db.AspNetUsers.Where(x=>x.OrganizationId == GetCurrentUserAsync().Result.OrganizationId);
            return View(usersList);
        }
    }
}
