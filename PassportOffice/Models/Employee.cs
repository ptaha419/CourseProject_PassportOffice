using System.Reflection.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace PassportOffice.Models
{
    public class Employee : User
    {
        [Key]
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Position { get; set; }
        [Required]
        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
