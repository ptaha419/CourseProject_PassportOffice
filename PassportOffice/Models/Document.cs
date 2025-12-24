using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public int? Number { get; set; }
        [Required]
        public string Authority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey(nameof(Application))]
        public int? ApplicationId { get; set; }
        public virtual Application Application { get; set; } 
    }
}

