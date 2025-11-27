namespace PassportOffice.Models
{
    public class Applicant : User
    {
        public User UserId { get; set; }
        public string BirthPlace { get; set; }
        public int TaxPayerNumber { get; set; }  // ИНН
        public string RegistrationAddress { get; set; }
        public string Photo { get; set; }
        public virtual ICollection<Document> DocumentId { get; set; } = new List<Document>();
        public virtual ICollection<Application> ApplicationId { get; set; } = new List<Application>();
    }
}
