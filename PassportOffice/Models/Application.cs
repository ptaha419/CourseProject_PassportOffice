using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace PassportOffice.Models
{
    public class Application
    {
        private int id;
        private Applicant applicant;
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
        public DateOnly StartDate;

        [DataType(DataType.Date)]
        public DateOnly EndDate;
        public string Status;

        [MaxLength(100, ErrorMessage = "Описание должно содержать максимум 100 символов.")]
        public string Description;
        private List<Document> AttachedDocs;
        private Employee employee;
        private string ApplicationReview;

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
            return id;
        }

        public void ChangeApplicationStatus(string NewStatus)
        {
            Status = NewStatus;
        }
    }
}
