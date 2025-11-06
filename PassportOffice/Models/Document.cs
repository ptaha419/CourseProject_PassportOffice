namespace PassportOffice.Models
{
    public class Document
    {
        private int id;
        public string typeOfDocument; 
        private string number; 
        protected string authority; 
        protected DateOnly startDate; 
        protected DateOnly endDate; 
    }
}
