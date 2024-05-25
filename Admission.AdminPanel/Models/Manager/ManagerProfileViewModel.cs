using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Admission.Application.Common.Mapping;
using Admission.DTOs.RpcModels.DictionaryService.GetFaculty;
using Admission.DTOs.RpcModels.UserService.GetManagerData;
using AutoMapper;

namespace Admission.AdminPanel.Models.Manager;

public sealed class ManagerProfileViewModel
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
    public Guid? FacultyId { get; init; }
    
    [Display(Name = "Факультет")]
    public string? FacultyName { get; init; }
}