using System.ComponentModel.DataAnnotations;
using Admission.Application.Common.Mapping;
using Admission.DTOs.RpcModels.DictionaryService.GetFaculty;
using Admission.DTOs.RpcModels.UserService.GetManagerData;

namespace Admission.AdminPanel.Models.Manager;

public sealed class ManagerProfileViewModel: IMapFrom<ManagerDataResponse>
{
    public Guid ManagerId { get; init; }
    
    [Display(Name = "Email")]
    [Required(ErrorMessage = "Поле обязательно")]
    [DataType(DataType.EmailAddress)]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    public string Email { get; init; } 
    
    [Display(Name = "ФИО")]
    [Required(ErrorMessage = "Поле обязательно")]
    [DataType(DataType.Text)]
    public string FullName { get; init; }
    
    
    [Display(Name = "Факультет")]
    [DataType(DataType.Text)]
    public FacultyResponse? Faculty { get; init; }
}