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
        public int Id;
        public string Title { get; set; }
        public string Text { get; set; }
        public Applicant? Applicant { get; set; }
        public int ApplicantId { get; set; }
        public Employee? Employee { get; set; }
        public int EmployeeId { get; set; }
        public Application? Application { get; set; }
        public int ApplicationId { get; set; }
    }
}
