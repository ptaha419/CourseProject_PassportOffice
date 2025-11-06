namespace PassportOffice.Models
{
    public class User
    {
        private int id;
        private string login; 
        private string password;
        public string surname;
        public string middleName;
        public string name;
        public DateOnly birthDate;
        public string gender;
        protected string phoneNumber; 
        protected string email; 
    }
}
