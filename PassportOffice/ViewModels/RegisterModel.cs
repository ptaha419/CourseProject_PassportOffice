using System.ComponentModel.DataAnnotations;

namespace PassportOffice.ViewModels
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указана фамилия")]
        public string Surname { get; set; }

        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Не указано имя")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана дата рождения")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Не указан пол")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Не указан номер телефона")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Не указана электронная почта")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Не выбрана роль пользователя")]
        public int RoleId { get; set; }

        [Required(ErrorMessage = "Не указано место рождения")]
        public string BirthPlace { get; set; }

        [Required(ErrorMessage = "Не указан ИНН")]
        public string TaxPayerNumber { get; set; }  // ИНН 

        [Required(ErrorMessage = "Не указан адрес регистрации")]
        public string RegistrationAddress { get; set; }
    }
}
