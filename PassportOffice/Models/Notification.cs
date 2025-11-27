using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace PassportOffice.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id;
        public string Title { get; set; }
        public string Text { get; set; }
        public Applicant ApplicantId { get; set; }
        public Employee EmployeeId { get; set; }
        public Application ApplicationId { get; set; }
    }
}
