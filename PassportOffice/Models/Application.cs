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
        public enum TypeOfApplication 
        {
            [Display(Name = "Выдача и замена паспорта")]
            IssuingAndReplacingPassport,

            //[Display(Name = "Оформление загранпаспорта")]
            //ForeignPassportProcessing,

            //[Display(Name = "Выдача вида на жительство")]
            //IssuanceResidencePermit,

            [Display(Name = "Регистрация по месту пребывания")]
            RegistrationByTemporaryResidence,

            [Display(Name = "Регистрация по месту жительства")]
            RegistrationByPermanentResidence,

            [Display(Name = "Снятие с регистрационного учета")]
            Deregistration
        }

        [Required(ErrorMessage = "Необходимо выбрать тип заявления")]
        public TypeOfApplication typeOfApplication { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Дата подачи заявления:")]
        public DateOnly StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateOnly EndDate { get; set; }

        public string Status { get; set; }

        [MaxLength(100, ErrorMessage = "Описание должно содержать максимум 100 символов.")]
        public string Description { get; set; }
        public List<Document> AttachedDocs { get; set; }
        public Employee employee { get; set; }
        public string ApplicationReview { get; set; }

        public Application() {}

        //public Application(int id, Applicant applicant, TypeOfApplication typeOfApp,
        //    DateOnly startDate, DateOnly endDate, string status, string description,
        //    List<Document> attachedDocs, Employee employee, string applicationReview)
        //{
        //    this.id = id;
        //    this.applicant = applicant;
        //    this.typeOfApplication = typeOfApplication;
        //    this.StartDate = startDate;
        //    this.EndDate = endDate;
        //    this.Status = status;
        //    this.Description = description;
        //    this.AttachedDocs = attachedDocs;
        //    this.employee = employee;
        //    this.ApplicationReview = applicationReview;
        //}

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
