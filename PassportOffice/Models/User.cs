using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Xml.Linq;

namespace PassportOffice.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id;
        public string Login { get; set; }
        public string Password { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    } 
}
