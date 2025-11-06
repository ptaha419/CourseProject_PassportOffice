namespace PassportOffice.Models
{
    public class Department
    {
        private int id;
        public string name;
        public string address;
        public string phoneNumber;
        public string email;
        public TimeOnly workingHours; 
        protected Employee employee;
        public string services; 
    }
}
