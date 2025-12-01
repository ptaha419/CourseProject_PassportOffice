using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace PassportOffice.Models
{
    public class Application
    {
        [Key]
        public int Id { get; set; }

        // Внешний ключ на заявителя
        [ForeignKey(nameof(Applicant))]
        public Guid ApplicantId { get; set; }
        public virtual Applicant Applicant { get; set; }

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

        // Внешний ключ на сотрудника
        [ForeignKey(nameof(Employee))]
        public Guid EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public string ApplicationReview { get; set; }
    }
}
