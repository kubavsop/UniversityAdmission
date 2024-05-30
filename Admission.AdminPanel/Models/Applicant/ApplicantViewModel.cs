using System.ComponentModel.DataAnnotations;
using Admission.Application.Common.Mapping;
using Admission.Application.Common.ValidationAttributes;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels.UserService.GetApplicantData;

namespace Admission.AdminPanel.Models.Applicant;

public sealed class ApplicantViewModel: IMapFrom<ApplicantDataResponse>
{
    [Display(Name = "Email")]
    [Required(ErrorMessage = "Поле обязательно")]
    [DataType(DataType.EmailAddress)]
    [MaxLength(1000)]
    [EmailAddress(ErrorMessage = "Некорректный email")]
    public string Email { get; init; }
    
    [Display(Name = "ФИО")]
    [Required(ErrorMessage = "Поле обязательно")]
    [DataType(DataType.Text)]
    [MinLength(1)]
    [MaxLength(1000)]
    public string FullName { get; init; }
    public Guid ApplicantId { get; init; }
    
    [Birthday]
    public DateTime? Birthday { get; init; }
    
    public Gender? Gender { get; init; }
    
    [PhoneNumber]
    public string? PhoneNumber { get; init; } 
    
    [MaxLength(1000)]
    public string? Citizenship { get; init; }
    public bool IsEditable { get; init; }
}