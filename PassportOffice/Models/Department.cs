using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassportOffice.Models
{
    public class Department
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public TimeOnly WorkingHours { get; set; }
        public ICollection<Employee> EmployeeId { get; set; } = new List<Employee>();
        public ICollection<TypeOfApplication> ServiceId { get; set; } = new List<TypeOfApplication>();
    }
}
