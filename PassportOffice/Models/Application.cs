using System.Security.Cryptography;

namespace PassportOffice.Models
{
    public class Application
    {
        private int id;
        private Applicant applicant;
        public enum TypeOfApplication 
        {
            IssuingAndReplacingPassport, // Выдача и смена паспорта
            ForeignPassportProcessing, // Оформление загранпаспорта
            IssuanceResidencePermit, // Выдача вида на жительство 
            RegistrationByTemporaryResidence, // Регистрация по месту пребывания 
            RegistrationByPermanentResidence, // Регистрация по месту жительства 
            Deregistration // Снятие с регистрационного учёта
        }

        public TypeOfApplication typeOfApplication { get; set; }
        public DateOnly StartDate;
        public DateOnly EndDate;
        public string Status;
        public string Description;
        private List<Document> AttachedDocs;
        private Employee employee;
        private string ApplicationReview;

        public Application(int id, Applicant applicant, TypeOfApplication typeOfApp,
            DateOnly startDate, DateOnly endDate, string status, string description,
            List<Document> attachedDocs, Employee employee, string applicationReview)
        {
            this.id = id;
            this.applicant = applicant; 
            this.typeOfApplication = typeOfApplication; 
            this.StartDate = startDate; 
            this.EndDate = endDate;
            this.Status = status; 
            this.Description = description; 
            this.AttachedDocs = attachedDocs; 
            this.employee = employee; 
            this.ApplicationReview = applicationReview; 
        }

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
