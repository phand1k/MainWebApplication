using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MainWebApplication.Areas.Identity.Data;
namespace MainWebApplication.Models
{
    public class Specialist
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Специалист")]
        public string Name { get; set; }
        [ForeignKey("OrganizationId")]
        public string? OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

    }
}
