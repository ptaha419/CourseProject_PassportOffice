namespace PassportOffice.Models
{
    public class Document
    {
        private int id;
        private string number;
        protected string authority; 
        protected DateOnly startDate; 
        protected DateOnly endDate; 
    }
}
