using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace PassportOffice.Models
{
    public class Notification
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }

        // Внешний ключ на заявку
        [ForeignKey(nameof(Application))]
        public int ApplicationId { get; set; }
        public virtual Application Application { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public bool IsRead { get; set; } = false;
    }
}
