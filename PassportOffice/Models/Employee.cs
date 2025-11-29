using System.Reflection.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace PassportOffice.Models
{
    public class Employee : User
    {
        [Key]
        public User? User { get; set; }
        public int UserId { get; set; }
        public string Position { get; set; }
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
    }
}
