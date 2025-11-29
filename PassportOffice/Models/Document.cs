using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using static PassportOffice.Models.Document;

namespace PassportOffice.Models
{
    public class Document
    {
        [Key]
        public int Id;
        public TypeOfDocument? TypeOfDocument { get; set; }
        public int TypeOfDocumentId { get; set; }
        public int Number { get; set; }
        public string Authority { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}

