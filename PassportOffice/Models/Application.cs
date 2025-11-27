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
        public Applicant applicant { get; set; }

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
        public Employee employee { get; set; }
        public string ApplicationReview { get; set; }

        public Application() {}

        public Application(int Id, Applicant applicant, TypeOfApplication typeOfApp,
            DateOnly startDate, DateOnly endDate, string status, string description,
            List<Document> attachedDocs, Employee employee, string applicationReview)
        {
            this.Id = Id;
            this.applicant = applicant;
            this.TypeOfApplicationId = TypeOfApplicationId;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.Status = status;
            this.Description = description;
            this.AttachedDocId = AttachedDocId;
            this.employee = employee;
            this.ApplicationReview = applicationReview;
        }

        public int GetId()
        {
            return Id;
        }

        public void ChangeApplicationStatus(string NewStatus)
        {
            Status = NewStatus;
        }
    }
}
