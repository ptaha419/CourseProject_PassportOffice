using Microsoft.EntityFrameworkCore;
using System;

namespace PassportOffice.Models
{
    public class WebAppDbContext : DbContext
    {
        public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options)
        {
        } 

        public DbSet<User> Users { get; set; } 
        public DbSet<Applicant> Applicants { get; set; } 
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<TypeOfDocument> TypesOfDocument { get; set; } 
        public DbSet<TypeOfApplication> TypesOfApplication { get; set; } 
        public DbSet<Status> Statuses { get; set; } 
        public DbSet<Department> Departments { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}
