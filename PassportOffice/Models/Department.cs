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

        // Навигационные свойства сотрудников и услуг
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
