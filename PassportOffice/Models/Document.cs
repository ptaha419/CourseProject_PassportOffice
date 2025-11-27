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
        public int Id;
        public enum TypeOfDocument
        {
            Passport,
            BirthCertificate
        }
        public TypeOfDocument documentType { get; set; }
        public int Number { get; set; }
        public string Authority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

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

