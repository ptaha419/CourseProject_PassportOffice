using System.ComponentModel.DataAnnotations;

namespace PassportOffice.Models
{
    public class TypeOfApplication
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
