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

        [Required]
        [ForeignKey(nameof(Role))]
        public int? RoleId { get; set; }
        public virtual Role Role { get; set; }

        [Required]
        public string BirthPlace { get; set; }
        [Required]
        public string TaxPayerNumber { get; set; }  // ИНН 
        [Required]
        public string RegistrationAddress { get; set; }
        public string Photo { get; set; }
        public string Position { get; set; }

        // Внешний ключ на отдел
        [ForeignKey(nameof(Department))]
        public int? DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
