using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography; 

namespace PassportOffice.Models
{
    public class Applicant : User
    {
        [Key]
        public User? User { get; set; }
        public int UserId { get; set; } 
        public string BirthPlace { get; set; }
        public int TaxPayerNumber { get; set; }  // ИНН
        public string RegistrationAddress { get; set; }
        public string Photo { get; set; }
        public ICollection<Document> DocumentId { get; set; } = new List<Document>();
        public ICollection<Application> ApplicationId { get; set; } = new List<Application>();
    }
}
