using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace PassportOffice.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }

        // Внешний ключ на вид обращения
        [ForeignKey(nameof(TypeOfApplication))]
        public int TypeOfApplicationId { get; set; }
        public virtual TypeOfApplication TypeOfApplication { get; set; }

        [ForeignKey(nameof(Status))]
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        [Required]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public string Description { get; set; }

        // Коллекция документов
        public virtual ICollection<Document> AttachedDocuments { get; set; } = new List<Document>();

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public string ApplicationReview { get; set; }
    }
}
