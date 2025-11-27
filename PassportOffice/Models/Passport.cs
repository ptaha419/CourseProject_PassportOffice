namespace PassportOffice.Models
{
    public class Passport : Document
    {
        private string citizenship { get; set; }

        public Passport(int id, TypeOfDocument docType, int number, string authority, DateTime startDate, DateTime endDate, string citizen) 
            : base(id, docType, number, authority, startDate, endDate)
        {
            this.citizenship = citizen;
        }
    }
}
