using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassportOffice.Models
{
    public class Department
    {
        [Key]
        public int Id;
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public TimeOnly WorkingHours { get; set; }
        public ICollection<Employee> EmployeeId { get; set; } = new List<Employee>();
        public ICollection<TypeOfApplication> ServiceId { get; set; } = new List<TypeOfApplication>();
    }
}
