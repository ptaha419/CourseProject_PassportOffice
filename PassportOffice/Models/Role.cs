using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PassportOffice.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        public string Name { get; set; }
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
