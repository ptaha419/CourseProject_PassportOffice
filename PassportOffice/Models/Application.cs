using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace PassportOffice.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("ApplicantId")]
        public Applicant? Applicant { get; set; }
        public int ApplicantId { get; set; }
        [Required]
        [ForeignKey("TypeOfApplicationId")]
        public TypeOfApplication? TypeOfApplication { get; set; }
        public int TypeOfApplicationId { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public ICollection<Document> AttachedDocId { get; set; } = new List<Document>();
        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }
        public int EmployeeId { get; set; }
        public string ApplicationReview { get; set; }
    }
}
