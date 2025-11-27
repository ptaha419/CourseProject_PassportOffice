using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace PassportOffice.Models
{
    public class Notification
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id;
        public string Title { get; set; }
        public string Text { get; set; }
        public Applicant applicant { get; set; }
        public Employee employee { get; set; }
        public Application application { get; set; }

        public void SendNotification(string Title, string Text, 
            Applicant applicant, Employee employee, Application application) 
        {
            this.Title = Title;
            this.Text = Text;
            this.applicant = applicant;
            this.employee = employee;
            this.application = application;

            Console.WriteLine($"Отправлено уведомление № {Id} по заявлению № {application.GetId()}");
            Console.WriteLine($"Заявитель: {applicant.Surname}, Сотрудник: {employee.Surname}");
            Console.WriteLine($"Заголовок: {Title}\nСообщение: {Text}");
        }
    }
}
