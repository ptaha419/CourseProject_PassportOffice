using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace PassportOffice.Models
{
    public class TypeOfApplication
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
