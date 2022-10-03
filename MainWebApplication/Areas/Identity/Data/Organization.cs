using System.ComponentModel.DataAnnotations;
using MainWebApplication.Models;
namespace MainWebApplication.Areas.Identity.Data
{
    public class Organization
    {
        public string Id { get; set; }
        [Display(Name="Название организации")]
        public string Name { get; set; }
        [Display(Name="БИН/ИИН Организации")]
        public string Number { get; set; }
        [Display(Name="Дата создания")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [DataType(DataType.Password)]
        [Display(Name = "Кодовое слово")]
        public string Password { get; set; }
        public ICollection<AspNetUser> AspNetUsers { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public ICollection<Service> Services { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<Specialist> Specialists { get; set; }
        public Organization()
        {
            AspNetUsers = new List<AspNetUser>();
            Patients = new List<Patient>();
            Services = new List<Service>();
            Appointments = new List<Appointment>();
            Specialists = new List<Specialist>();
        }
    }
}
