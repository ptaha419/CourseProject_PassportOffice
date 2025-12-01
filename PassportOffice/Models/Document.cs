using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using static PassportOffice.Models.Document;

namespace PassportOffice.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }

        // Внешний ключ на тип документа
        [ForeignKey(nameof(TypeOfDocument))]
        public int TypeOfDocumentId { get; set; }
        public virtual TypeOfDocument TypeOfDocument { get; set; }

        [Required]
        public int Number { get; set; }
        [Required]
        public string Authority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Внешний ключ на заявителя
        [ForeignKey(nameof(Applicant))]
        public Guid ApplicantId { get; set; }
        public virtual Applicant Applicant { get; set; }
    }
}

