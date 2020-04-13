using System.ComponentModel.DataAnnotations;


namespace test.Models
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Не указан Email")]
        [Display(Name = "Логин(email)")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}
