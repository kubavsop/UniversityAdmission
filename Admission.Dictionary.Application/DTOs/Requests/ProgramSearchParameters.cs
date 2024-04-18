using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Admission.Dictionary.Application.DTOs.Requests;

public sealed class ProgramSearchParameters
{
    [DefaultValue(DefaultPage)]
    [Range(1, int.MaxValue)]
    public int Page { get; set; } = DefaultPage;
    
    [DefaultValue(DefaultSize)]
    [Range(1, int.MaxValue)]
    public int Size { get; set; } = DefaultSize;
    
    public ICollection<Guid> Faculties { get; set; } = new List<Guid>();
    
    public ICollection<int> EducationLevels { get; set; } = new List<int>();
    
    public string? EducationForm { get; set; }
    public string? Language { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    
    private const int DefaultPage = 1;
    private const int DefaultSize = 5;
}