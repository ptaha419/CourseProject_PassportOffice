using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Xml.Linq;

namespace PassportOffice.Models
{
    public class Department
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id;
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public TimeOnly WorkingHours { get; set; }
        public Employee employee { get; set; }
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
