using System.ComponentModel.DataAnnotations;

namespace Admission.AdminPanel.Models.Manager;

public sealed class ChangePasswordViewModel
{
    [Display(Name = "Старый пароль")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Поле обязательно")]
    public string OldPassword { get; set; }
    
    [Display(Name = "Новый пароль")]
    [DataType(DataType.Password)]
    [Required(ErrorMessage = "Поле обязательно")]
    public string NewPassword { get; init; }
}