using System.Security.Cryptography;

namespace PassportOffice.Models
{
    public class Application
    {
        private int id;
        private Applicant applicant;
        public enum typeOfApplication { };
        public DateOnly startDate;
        public DateOnly endDate;
        public string status;
        public string description;
        private List<Document> attachedDocs;
        private Employee employee;
        private string applicationReview;

        public Application(int id, Applicant applicant, Employee employee)
        {
            this.id = id;
            this.applicant = applicant;
            this.employee = employee;
        }

        public int GetId()
        {
            return id;
        }
    }
}
