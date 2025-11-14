using System.Reflection.Metadata;

namespace PassportOffice.Models
{
    public class Employee : User
    {
        public string position;
        public Department department;

        public Employee(int id, string login, string password, string phoneNumber, string email,
        string surname, string middleName, string name, DateOnly birthDate, string gender) : base(id, login, password, phoneNumber, email)
        {
            Surname = surname;
            MiddleName = middleName;
            Name = name;
            BirthDate = birthDate;
            Gender = gender; 
        }
    }
}
