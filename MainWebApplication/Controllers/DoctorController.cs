using Microsoft.AspNetCore.Mvc;
using MainWebApplication.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using MainWebApplication.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MainWebApplication.Controllers
{
    [Authorize(Roles = "Врач")]
    public class DoctorController : Controller
    {
        private UserManager<AspNetUser> _userManager;
        ApplicationDbContext db;
        public DoctorController(ApplicationDbContext context, UserManager<AspNetUser> userManager)
        {
            db = context;
            _userManager = userManager;
        }
        private Task<AspNetUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Home");
        }
        public IActionResult MyAppointments()
        {
            var listPatientsForAppointment = db.Appointments.Include(x=>x.Service).Include(x=>x.Patient).
                Where(x=>x.DoctorId == GetCurrentUserAsync().Result.Id);
            return View(listPatientsForAppointment.ToList());
        }
        [HttpGet]
        public IActionResult RegisterPatient()
        {
            ViewData["CityId"] = new SelectList(db.City, "Id", "Name");
            return View();
        }
        [HttpPost]
        public IActionResult RegisterPatient(Patient patient)
        {
            var checkPatient = db.Patients.FirstOrDefault(x=>x.IndividualNumber == patient.IndividualNumber);
            ViewData["CityId"] = new SelectList(db.City, "Id", "Name");
            if (checkPatient == null)
            {
                patient.MaleId = IndividualNumberPerson.MalePerson(patient.IndividualNumber);
                patient.BornDate = IndividualNumberPerson.BornDatePerson(patient.IndividualNumber);
                patient.OrganizationId = GetCurrentUserAsync().Result.OrganizationId;
                db.Patients.Add(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("IndividualNumber", "Пациент с таким ИИН уже существует");
            return View(patient);
        }
        public IActionResult AppointmentList()
        {
            var listAppointment = db.Appointments.Include(x=>x.AspNetUser).Include(x=>x.Organization).Include(x=>x.Service).Include(x=>x.Patient).
                Where(x=>x.OrganizationId == GetCurrentUserAsync().Result.OrganizationId);
            return View(listAppointment.ToList());
        }
        public ActionResult Appointment()
        {
            ViewData["DoctorId"] = new SelectList(db.AspNetUsers.
                Where(x=>x.OrganizationId == GetCurrentUserAsync().Result.OrganizationId), "Id", "FullName");
            ViewData["ServiceId"] = new SelectList(db.Services.
                Where(x => x.OrganizationId == GetCurrentUserAsync().Result.OrganizationId), "Id", "Name");
            return View();
        }
        [HttpPost]
        public ActionResult Appointment(Appointment appointment)
        {
            ViewData["DoctorId"] = new SelectList(db.AspNetUsers.
                Where(x => x.OrganizationId == GetCurrentUserAsync().Result.OrganizationId), "Id", "FullName");
            ViewData["ServiceId"] = new SelectList(db.Services.
                Where(x => x.OrganizationId == GetCurrentUserAsync().Result.OrganizationId), "Id", "Name");
            var checkPatient = db.Patients.FirstOrDefault(x=>x.IndividualNumber == appointment.IndividualNumberPatient);
            if (checkPatient != null)
            {
                appointment.PatientId = db.Patients.Where(x=>x.IndividualNumber == appointment.IndividualNumberPatient).Select(x=>x.Id).FirstOrDefault();
                appointment.OrganizationId = GetCurrentUserAsync().Result.OrganizationId;
                appointment.DateOfCreateAppointment = DateTime.Now;
                appointment.OrganizationId = GetCurrentUserAsync().Result.OrganizationId;
                db.Appointments.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ModelState.AddModelError("PatientId", "Пациент с таким ИИН не существует!");
            return View(appointment);
        }
        public IActionResult Test()
        {
            return View();
        }
        public IActionResult ListPatients()
        {
            var listPatients = db.Patients.Include(x=>x.Organization);
            return View(listPatients.ToList());
        }
        public IActionResult DetailsPatient(int? id)
        {
            Patient patient = db.Patients.
                Include(x=>x.Male).
                Include(x=>x.City).
                Include(x=>x.Organization).
                FirstOrDefault(x=>x.Id == id);
            return View(patient);
        }
        public async Task<IActionResult> EditPatient(int? id)
        {
            ViewData["CityId"] = new SelectList(db.City, "Id", "Name");
            Patient patient = await db.Patients.FindAsync(id);
            return View(patient);
        }
        [HttpPost]
        public async Task<IActionResult> EditPatient(Patient patient)
        {
            patient.OrganizationId = patient.OrganizationId;
            ViewData["CityId"] = new SelectList(db.City, "Id", "Name");
            db.Entry(patient).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return View(patient);
        }
    }
}
