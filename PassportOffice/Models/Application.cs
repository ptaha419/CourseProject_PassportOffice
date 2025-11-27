using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;

namespace PassportOffice.Models
{
    public class Application
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id;
        public Applicant ApplicantId { get; set; }

        [Required(ErrorMessage = "Необходимо выбрать тип заявления")]
        public TypeOfApplication TypeOfApplicationId { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата подачи заявления:")]
        public DateOnly StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateOnly EndDate { get; set; }

        public string Status { get; set; }

        [MaxLength(100, ErrorMessage = "Описание должно содержать максимум 100 символов.")]
        public string Description { get; set; }
        public ICollection<Document> AttachedDocId { get; set; } = new List<Document>(); 
        public Employee EmployeeId { get; set; }
        public string ApplicationReview { get; set; }
    }
}
