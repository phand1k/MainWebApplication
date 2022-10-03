using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MainWebApplication.Models;
namespace MainWebApplication.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AspNetUser class
public class AspNetUser : IdentityUser
{
    [Display(Name ="Имя")]
    public string? FirstName { get; set; }
    [Display(Name = "Фамилия")]
    public string? LastName { get; set; }
    public DateTime DateOfCreated { get; set; } = DateTime.Now;
    [Required]
    [ForeignKey("OrganizationId")]
    public string OrganizationId { get; set; }
    public virtual Organization Organization { get; set; }
    public ICollection<Appointment> Appointments { get; set; }
    public AspNetUser()
    {
        Appointments = new List<Appointment>();
    }
    public string FullName
    {
        get
        {
            return FirstName + " " + LastName;
        }
    }
}

