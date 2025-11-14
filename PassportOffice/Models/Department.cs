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
        public enum Services
        {
            IssuingAndReplacingPassport, // Выдача и смена паспорта
            ForeignPassportProcessing, // Оформление загранпаспорта
            IssuanceResidencePermit, // Выдача вида на жительство 
            RegistrationByTemporaryResidence, // Регистрация по месту пребывания 
            RegistrationByPermanentResidence, // Регистрация по месту жительства 
            Deregistration // Снятие с регистрационного учёта
        }

        public HashSet<Services> AvailableServices { get; set; } = new HashSet<Services>();
        public void GetDepartmentInfo()
        {
            Console.WriteLine($"Название: {Name}");
            Console.WriteLine($"Адрес: {Address}");
            Console.WriteLine($"Телефон: {PhoneNumber}");
            Console.WriteLine($"Email: {Email}");
            Console.WriteLine($"Рабочие часы: {WorkingHours.ToString()}");
            Console.WriteLine("Доступные услуги:");
            foreach (var service in AvailableServices)
            {
                Console.WriteLine(service);
            }
        }

        public List<Employee> Employees { get; set; } = new List<Employee>();
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
