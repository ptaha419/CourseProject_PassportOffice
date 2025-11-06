namespace PassportOffice.Models
{
    public class Applicant : User
    {
        private string birthPlace;
        private int taxPayerNumber;
        protected string registrationAddress;
        protected string photo;
        protected Document documentId;  
    }
}
