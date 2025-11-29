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

        // Навигационные свойства
        public virtual ICollection<Document> Documents { get; set; } = new List<Document>();
        public virtual ICollection<Application> Applications { get; set; } = new List<Application>();
    }
}
