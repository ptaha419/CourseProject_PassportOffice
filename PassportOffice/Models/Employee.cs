using System.Reflection.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace PassportOffice.Models
{
    public class Employee : User
    {
        [Required]
        public string Position { get; set; }

        // Внешний ключ на отдел
        [ForeignKey(nameof(Department))]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
