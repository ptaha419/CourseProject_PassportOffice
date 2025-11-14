using static PassportOffice.Models.Document;
using System.Xml.Linq;

namespace PassportOffice.Models
{
    public class Document
    {
        private readonly int _id;
        public enum TypeOfDocument
        {
            Passport,
            BirthCertificate
        }
        private readonly TypeOfDocument _documentType;
        private readonly int _number;
        protected string Authority;
        protected DateTime StartDate;
        protected DateTime EndDate;

        public Document(int id, TypeOfDocument docType, int number, string auth,
                        DateTime start, DateTime end)
        {
            _id = id;
            _documentType = docType;
            _number = number;
            Authority = auth;
            StartDate = start;
            EndDate = end;
        }

        public TypeOfDocument GetTypeOfDocument()
        {
            return _documentType;
        }

        public override string ToString()
        {
            return $"Тип документа: {_documentType.ToString()}";
        }

        public int GetDocumentId(int _id) 
        { 
            return (_id) ;
        }

        public bool IsValid()
        {
            var today = DateTime.Today;
            return EndDate >= today;
        }
    }
}

