using PassportOffice.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassportOffice.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указана фамилия")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Не указано отчество")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана дата рождения")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Не указан пол")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Не указан номер телефона")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Не выбрана роль пользователя")]
        public int RoleId { get; set; }

    }
}
