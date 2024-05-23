using System.ComponentModel.DataAnnotations;

namespace Admission.AdminPanel.Models;

public sealed class RegisterViewModel
{
    [Display(Name = "ФИО")]
    [Required(ErrorMessage = "Поле обязательно")]
    [DataType(DataType.Text)]
    public string FullName { get; set; }
    
    [Display(Name = "Email")]
    [Required(ErrorMessage = "Поле обязательно")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    public string Email { get; set; }

    [Display(Name = "Пароль")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Поле обязательно")]
    public string Password { get; set; }
}