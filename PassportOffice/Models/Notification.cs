using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace PassportOffice.Models
{
    public class Notification
    {
        private int id;
        private string Title;
        private string Text;
        private Applicant applicant;
        private Employee employee;
        private Application application;

        public void SendNotification(string Title, string Text, 
            Applicant applicant, Employee employee, Application application) 
        {
            this.Title = Title;
            this.Text = Text;
            this.applicant = applicant;
            this.employee = employee;
            this.application = application;

            Console.WriteLine($"Отправлено уведомление № {id} по заявлению № {application.GetId()}");
            Console.WriteLine($"Заявитель: {applicant.name}, Сотрудник: {employee.name}");
            Console.WriteLine($"Заголовок: {Title}\nСообщение: {Text}");
        }
    }
}
