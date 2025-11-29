using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassportOffice.Models
{
    public class Department
    {
        [Key]
        public int Id;
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
        [ForeignKey("EmployeeId")]
        public ICollection<Employee> EmployeeId { get; set; } = new List<Employee>();
        [ForeignKey("ServiceId")]
        public ICollection<TypeOfApplication> ServiceId { get; set; } = new List<TypeOfApplication>();
    }
}
