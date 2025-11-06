namespace PassportOffice.Models
{
    public class Application
    {
        private int id;
        private Applicant applicantId;
        public enum typeOfApplication { };
        public DateOnly startDate; 
        public DateOnly endDate;
        public string status;
        public string description; 
        private List<Document> attachedDocs;
        private Employee employee;
        private string applicationReview; 
    }
}
