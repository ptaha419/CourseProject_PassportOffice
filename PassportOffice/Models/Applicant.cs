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

        public List<Document> Documents { get; set; } = new List<Document>();
        public void GetAllDocuments()
        {
            if (Documents.Count > 0)
            {
                foreach (var doc in Documents)
                    Console.WriteLine(doc);
            }
            else
            {
                Console.WriteLine("Документы не найдены.");
            }
        }

        public List<Application> Applications { get; set; } = new List<Application>();
        public void GetListOfApplications()
        {
            if (Applications.Count > 0)
            {
                foreach (var app in Applications)
                    Console.WriteLine(app);
            }
            else
            {
                Console.WriteLine("Заявления не найдены.");
            }
        }

        public void ChangeRegistrationAddress(string NewRegistrationAddress) 
        {
            RegistrationAddress = NewRegistrationAddress; 
        } 
    }
}
