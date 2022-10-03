using System.ComponentModel.DataAnnotations;

namespace MainWebApplication.Models
{
    public class Male
    {
        public int Id { get; set; }
        [Display(Name="Пол")]
        public string Name { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public Male()
        {
            Patients = new List<Patient>();
        }
    }
}
