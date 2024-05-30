using System.ComponentModel.DataAnnotations;
using Admission.Application.Common.ValidationAttributes;

namespace Admission.AdminPanel.Models.Applicant;

public class DocumentViewModel
{
    public Guid DocumentId { get; init; }
    
    [Display(Name = "Файл")]
    [AllowedFileExtensions(ErrorMessage = "Возможные расширения: .png, .pdf, .jpg, .jpeg, .jpe")]
    [FileMaxSize(1024 * 1024 * 10, ErrorMessage = "Файл слишком большой")]
    [Required(ErrorMessage = "Поле обязательно")]
    public IFormFile? File { get; init; }
}