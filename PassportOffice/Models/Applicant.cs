using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.SymbolStore;
using System.Security.Cryptography; 

namespace PassportOffice.Models
{
    public class Applicant : User
    {
        [Required]
        public string BirthPlace { get; set; }
        [Required]
        public string TaxPayerNumber { get; set; }  // ИНН 
        [Required]
        public string RegistrationAddress { get; set; }
        public string Photo { get; set; }
        public ICollection<Document> DocumentId { get; set; } = new List<Document>();
        public ICollection<Application> ApplicationId { get; set; } = new List<Application>();
    }
}
