using MainWebApplication.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MainWebApplication.Models;
namespace MainWebApplication.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<AspNetUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Organization> Organization { get; set; }
    public DbSet<AspNetUser> AspNetUsers { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<City> City { get; set; }
    public DbSet<Analysis> Analyses { get; set; }
    public DbSet<Male> Male { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Specialist> Specialists { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
