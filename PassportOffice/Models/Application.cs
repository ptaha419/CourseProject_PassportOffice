using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace PassportOffice.Models
{
    public class Application
    {
        [Key]
        public int Id;
        public Applicant? Applicant { get; set; }
        public int ApplicantId { get; set; }
        public TypeOfApplication? TypeOfApplication { get; set; }
        public int TypeOfApplicationId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public ICollection<Document> AttachedDocId { get; set; } = new List<Document>();
        public Employee? Employee { get; set; }
        public int EmployeeId { get; set; }
        public string ApplicationReview { get; set; }
    }
}
