using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace PassportOffice.Models
{
    public class User : IdentityUser
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        //public int Id { get; set; }

        [Required]
        public string Surname { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; } 

        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual ICollection<Applicant> Applicants { get; set; } = new List<Applicant>();
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }
}
