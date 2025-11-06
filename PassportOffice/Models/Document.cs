namespace PassportOffice.Models
{
    public class Document
    {
        private int id;
        public enum typeOfDocument { }; 
        private string number; 
        protected string authority; 
        protected DateOnly startDate; 
        protected DateOnly endDate;
        protected Applicant applicantId;
    }
}
