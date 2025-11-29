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
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        [ForeignKey("ApplicationId")]
        public Applicant? Applicant { get; set; }
        public int ApplicantId { get; set; }
        [Required]
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
        public int EmployeeId { get; set; }
        [Required]
        [ForeignKey("ApplicationId")]
        public Application? Application { get; set; }
        public int ApplicationId { get; set; }
    }
}
