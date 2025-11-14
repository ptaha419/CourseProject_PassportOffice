namespace PassportOffice.Models
{
    public class Applicant : User
    {
        private string BirthPlace;
        private int TaxPayerNumber;
        protected string RegistrationAddress;
        protected string Photo;
        protected Document document;

        public Applicant(int id, string login, string password, string phoneNumber, string email,
                string surname, string middleName, string name, DateOnly birthDate, string gender,
                string birthPlace, int taxPayerNumber, string registrationAddress, string photo, Document doc)
        : base(id, login, password, phoneNumber, email)
        {
            Surname = surname;
            MiddleName = middleName;
            Name = name;
            BirthDate = birthDate;
            Gender = gender;
            BirthPlace = birthPlace;
            TaxPayerNumber = taxPayerNumber;
            RegistrationAddress = registrationAddress;
            Photo = photo;
            document = doc;
        }
    }
}
