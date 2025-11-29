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
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }

        // Внешний ключ на заявку
        [ForeignKey(nameof(Application))]
        public int ApplicationId { get; set; }
        public virtual Application Application { get; set; }

        // Внешний ключ на сотрудника
        [ForeignKey(nameof(Employee))]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        // Внешний ключ на заявителя
        [ForeignKey(nameof(Applicant))]
        public int ApplicantId { get; set; }
        public virtual Applicant Applicant { get; set; }
    }
}
