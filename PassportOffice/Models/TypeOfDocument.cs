using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassportOffice.Models
{
    public class TypeOfDocument
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
