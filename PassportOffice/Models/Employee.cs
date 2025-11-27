using System.Reflection.Metadata;

namespace PassportOffice.Models
{
    public class Employee : User
    {
        public string Position { get; set; }
        public Department DepartmentId { get; set; }
    }
}
