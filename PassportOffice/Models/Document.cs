using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using static PassportOffice.Models.Document;

namespace PassportOffice.Models
{
    public class Document
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        private readonly int Id;
        public enum TypeOfDocument
        {
            Passport,
            BirthCertificate
        }
        private readonly TypeOfDocument documentType;
        private readonly int Number;
        protected string Authority { get; set; }
        protected DateTime StartDate { get; set; }
        protected DateTime EndDate { get; set; }

        public Document(int id, TypeOfDocument docType, int number, string auth,
                        DateTime start, DateTime end)
        {
            Id = id;
            documentType = docType;
            Number = number;
            Authority = auth;
            StartDate = start;
            EndDate = end;
        }

        public TypeOfDocument GetTypeOfDocument()
        {
            return documentType;
        }

        public override string ToString()
        {
            return $"Тип документа: {documentType.ToString()}";
        }

        public int GetDocumentId(int Id) 
        { 
            return (Id) ;
        }

        public bool IsValid()
        {
            var today = DateTime.Today;
            return EndDate >= today;
        }
    }
}

