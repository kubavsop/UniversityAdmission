using System.ComponentModel.DataAnnotations;
using Admission.Application.Common.Mapping;
using Admission.Application.Common.ValidationAttributes;
using Admission.Document.Application.ValidationAttributes;
using Admission.DTOs.RpcModels.DocumentService;
using Admission.DTOs.RpcModels.DocumentService.GetApplicantPassport;

namespace Admission.AdminPanel.Models.Applicant;

public sealed class PassportViewModel: DocumentViewModel, IMapFrom<PassportResponse>
{
    
    [Display(Name = "Серия")]
    [Required(ErrorMessage = "Поле обязательно")]
    [Range(1000, 9999)] 
    public int? Series { get; init; }
    
    [Display(Name = "Номер")]
    [Required(ErrorMessage = "Поле обязательно")]
    [Range(100000, 999999)]
    public int? Number { get; init; }
    
    [Display(Name = "Место рождения")]
    [Required(ErrorMessage = "Поле обязательно")]
    [MinLength(1)]
    [MaxLength(400)]
    public string PlaceOfBirth { get; init; }
    
    [Display(Name = "Кем выдан")]
    [Required(ErrorMessage = "Поле обязательно")]
    [MinLength(1)]
    [MaxLength(400)]
    public string IssuedBy { get; init; }
    
    [Display(Name = "Дата выдачи")]
    [Required(ErrorMessage = "Поле обязательно")]
    [DateIssued]
    public DateTime? DateIssued { get; init; }

    public IEnumerable<ScanRpcModel> Scans { get; init; } = new List<ScanRpcModel>();
    
    public bool IsEditable { get; init; }
}