using System.ComponentModel.DataAnnotations;

namespace test.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указано имя")]
        [Display(Name = "Имя*")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Не указана фамилия")]
        [Display(Name = "Фамилия*")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Не указано отчество")]
        [Display(Name = "Отчество")]
        public string Patronymic { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        [Display(Name = "Логин(email)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        [Display(Name = "Повторите пароль")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Телефон")]
        public string Phonenumber { get; set; }

        [Required(ErrorMessage = "Обязательное требование")]
        public bool Agreement { get; set; }

        [Required(ErrorMessage = "Обязательное требование")]
        public bool UserData { get; set; }

        public string ReturnUrl { get; set; }
    }
}
