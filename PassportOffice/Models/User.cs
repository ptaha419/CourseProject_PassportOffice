using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Xml.Linq;

namespace PassportOffice.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
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
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        public string Password { get; set; } 

        [NotMapped]
        [Required]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }

        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
