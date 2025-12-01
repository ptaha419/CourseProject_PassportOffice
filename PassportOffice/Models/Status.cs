using System.ComponentModel.DataAnnotations;

namespace PassportOffice.Models
{
    public class Status
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
