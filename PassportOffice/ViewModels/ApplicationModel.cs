using PassportOffice.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassportOffice.ViewModels
{
    public class ApplicationModel
    {
        [Required(ErrorMessage = "Не указан тип заявления")]
        public int TypeOfApplicationId { get; set; }
        public int StatusId { get; set; }
        [Required(ErrorMessage = "Не указана дата")]
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "Текст заявления отсутствует")]
        public string Description { get; set; }
        public Guid UserId { get; set; }

        public string ApplicationReview { get; set; }
    }
}
