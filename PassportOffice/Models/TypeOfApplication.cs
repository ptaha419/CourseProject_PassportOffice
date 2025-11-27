using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace PassportOffice.Models
{
    public class TypeOfApplication
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public TypeOfApplication(int Id, string name)
        {
            this.Id = Id;
            this.Name = name;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
