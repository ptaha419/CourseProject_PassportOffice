using System.Net;
using System.Xml.Linq;

namespace PassportOffice.Models
{
    public class Department
    {
        private int id;
        public string Name;
        public string Address;
        public string PhoneNumber;
        public string Email;
        public TimeOnly WorkingHours; 
        protected Employee employee;
        public enum services { }

        public List<Employee> Employees { get; set; } = new List<Employee>(); 
        public void GetDepartmentInfo()
        {
            Console.WriteLine($"Название: {Name}");
            Console.WriteLine($"Адрес: {Address}");
            Console.WriteLine($"Телефон: {PhoneNumber}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Рабочие часы: {WorkingHours.ToString()}");
        }

        public void GetListOfEmployees()
        {
            if (Employees.Count > 0)
            {
                foreach (var emp in Employees)
                    Console.WriteLine(emp);
            }
            else
            {
                Console.WriteLine("Сотрудники отдела не найдены.");
            }
        }
    }
}
