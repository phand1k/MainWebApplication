using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MainWebApplication.Areas.Identity.Data;
namespace MainWebApplication.Models
{
    public class Patient
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "ИИН пациента")]
        [StringLength(12, MinimumLength = 12)]
        public string? IndividualNumber { get; set; }
        [Required]
        [Display(Name = "Имя")]
        public string? FirstName { get; set; }
        [Required]
        [Display(Name = "Фамилия")]
        public string? LastName { get; set; }
        [DataType(DataType.Date)]
        [Display(Name ="Дата рождения")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BornDate { get; set; }
        [Display(Name = "Адрес")]
        public string? Address { get; set; }
        [Required]
        [ForeignKey("CityId")]
        [Display(Name = "Город")]
        public int CityId { get; set; }
        public virtual City City { get; set; }
        [Phone]
        [Display(Name ="Телефон")]
        public string? PhoneNumber { get; set; }
        [Required]
        [ForeignKey("OrganizationId")]
        [Display(Name = "Обслуживающая организация")]
        public string? OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
        [Required]
        [ForeignKey("MaleId")]
        public int? MaleId { get; set; }
        public virtual Male Male { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public Patient()
        {
            Appointments = new List<Appointment>();
        }
    }
}
