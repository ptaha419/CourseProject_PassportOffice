using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography; 

namespace PassportOffice.Models
{
    public class Applicant : User
    {
        [Key]
        [ForeignKey("UserId")]
        public User? User { get; set; }
        public int UserId { get; set; }
        [Required]
        public string BirthPlace { get; set; }
        [Required]
        public int TaxPayerNumber { get; set; }  // ИНН 
        [Required]
        public string RegistrationAddress { get; set; }
        public string Photo { get; set; }
        public ICollection<Document> DocumentId { get; set; } = new List<Document>();
        public ICollection<Application> ApplicationId { get; set; } = new List<Application>();
    }
}
