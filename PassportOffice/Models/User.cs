using System.Net;
using System.Xml.Linq;

namespace PassportOffice.Models
{
    public class User
    {
        private int id;
        private string Login; 
        private string Password;
        public string Surname;
        public string MiddleName;
        public string Name;
        public DateOnly BirthDate;
        public string Gender;
        protected string PhoneNumber; 
        protected string Email;

        public User(int id, string Login, string Password, string PhoneNumber, string Email)
        {
            this.id = id;
            this.Login = Login;
            this.Password = Password; 
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
        }

        public int GetId()
        {
            return id;
        }

        public void GetUserInfo()
        {
            Console.WriteLine($"ID: {id}");
            Console.WriteLine($"Имя: {Name}"); 
            Console.WriteLine($"Отчество: {MiddleName}"); 
            Console.WriteLine($"Фамилия: {Surname}"); 
            Console.WriteLine($"Дата рождения: {BirthDate}"); 
            Console.WriteLine($"Пол: {Gender}"); 
            Console.WriteLine($"Телефон: {PhoneNumber}");
            Console.WriteLine($"Email: {Email}");
        }

        public string GetFullName()
        {
            return $"{Surname} {MiddleName} {Name}";
        }
    } 
}
