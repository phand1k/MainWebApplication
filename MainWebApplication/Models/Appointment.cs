using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MainWebApplication.Areas.Identity.Data;
namespace MainWebApplication.Models
{
    public class Appointment
    {
        [Required(ErrorMessage = "Введите ИИН пациента")]
        [Display(Name = "ИИН пациента")]
        public string IndividualNumberPatient { get; set; }
        public int Id { get; set; }
        [ForeignKey("ServiceId")]
        [Display(Name = "Услуга")]
        public int? ServiceId { get; set; }
        public virtual Service Service { get; set; }
        [ForeignKey("PatientId")]
        [Display(Name = "Пациент")]
        public int? PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        [Display(Name = "Дата приема (услуги)")]
        public DateTime DateOfAppointment { get; set; }
        [Display(Name = "Запись создана")]
        public DateTime DateOfCreateAppointment { get; set; }
        [Required]
        [ForeignKey("DoctorId")]
        [Display(Name = "К врачу")]
        public string? DoctorId { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
        [ForeignKey("OrganizationId")]
        [Display(Name="Организация")]
        public string? OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

    }
}
