namespace PassportOffice.Models
{
    public class Applicant : User
    {
        private string BirthPlace { get; set; }
        private int TaxPayerNumber { get; set; }  // ИНН
        protected string RegistrationAddress { get; set; }
        protected string Photo { get; set; }
        public virtual ICollection<Document> Documents { get; set; } = new HashSet<Document>();
        public virtual ICollection<Application> Applications { get; set; } = new HashSet<Application>();

        public Applicant(int id, string login, string password, string phoneNumber, string email,
                         string surname, string middleName, string name, DateOnly birthDate, string gender,
                         string birthPlace, int taxPayerNumber, string registrationAddress, string photo)
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
        }

        public void GetApplicantInfo()
        {
            Console.WriteLine($"Место рождения: {BirthPlace}");
            Console.WriteLine($"ИНН: {TaxPayerNumber}");
            Console.WriteLine($"Адрес регистрации: {RegistrationAddress}");
            Console.WriteLine($"Фото: {Photo}");
        }

        public void ChangeRegistrationAddress(string newRegistrationAddress)
        {
            RegistrationAddress = newRegistrationAddress;
        }

        public void GetAllDocuments()
        {
            if (Documents.Any())
            {
                foreach (var doc in Documents)
                    Console.WriteLine(doc.ToString());
            }
            else
            {
                Console.WriteLine("Документы не найдены.");
            }
        }

        public void GetListOfApplications()
        {
            if (Applications.Any())
            {
                foreach (var app in Applications)
                    Console.WriteLine(app.ToString());
            }
            else
            {
                Console.WriteLine("Заявления не найдены.");
            }
        }
    }
}
