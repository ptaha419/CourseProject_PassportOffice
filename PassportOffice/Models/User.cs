using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Xml.Linq;

namespace PassportOffice.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private int Id;
        private string Login { get; set; }
        private string Password { get; set; }
        public string Surname { get; set; }
        public string MiddleName { get; set; }
        public string Name { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Gender { get; set; }
        protected string PhoneNumber { get; set; }
        protected string Email { get; set; }

        public User(int id, string Login, string Password, string PhoneNumber, string Email)
        {
            this.Id = id;
            this.Login = Login;
            this.Password = Password; 
            this.PhoneNumber = PhoneNumber;
            this.Email = Email;
        }

        public int GetId()
        {
            return Id;
        }

        public void GetUserInfo()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Имя: {Name}"); 
            Console.WriteLine($"Отчество: {MiddleName}"); 
            Console.WriteLine($"Фамилия: {Surname}"); 
            Console.WriteLine($"Дата рождения: {BirthDate}"); 
            Console.WriteLine($"Пол: {Gender}"); 
            Console.WriteLine($"Телефон: {PhoneNumber}");
            Console.WriteLine($"Email: {Email}");
        }

        public string GetFullName() => $"{Surname} {MiddleName} {Name}";
    } 
}
