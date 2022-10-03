using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MainWebApplication.Areas.Identity.Data;
namespace MainWebApplication.Models
{
    public class Service
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Название услуги")]
        public string Name { get; set; }
        [ForeignKey("OrganizationId")]
        public string? OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public Service()
        {
            Appointments = new List<Appointment>();
        }
    }
}
