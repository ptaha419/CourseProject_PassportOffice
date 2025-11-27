namespace PassportOffice.Models
{
    public class Applicant : User
    {
        public string BirthPlace { get; set; }
        public int TaxPayerNumber { get; set; }  // ИНН
        public string RegistrationAddress { get; set; }
        public string Photo { get; set; }
        public virtual ICollection<Document> DocumentId { get; set; } = new List<Document>();
        public virtual ICollection<Application> ApplicationId { get; set; } = new List<Application>();

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
            if (DocumentId.Any())
            {
                foreach (var doc in DocumentId)
                    Console.WriteLine(doc.ToString());
            }
            else
            {
                Console.WriteLine("Документы не найдены.");
            }
        }

        public void GetListOfApplications()
        {
            if (ApplicationId.Any())
            {
                foreach (var app in ApplicationId)
                    Console.WriteLine(app.ToString());
            }
            else
            {
                Console.WriteLine("Заявления не найдены.");
            }
        }
    }
}
