namespace Admission.Application.Common.Constants;

public static class ContentTypeMappings
{
    public static Dictionary<string, string> TypeMappings { get; } = new Dictionary<string, string>
    {
        { ".jpe", "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".jpg", "image/jpeg" },
        { ".pdf", "application/pdf" },
        { ".png", "image/png" }
    };
    
    public static Dictionary<string, string> ReverseTypeMappings { get; } = new Dictionary<string, string>
    {
        { "image/jpeg", ".jpe" },
        { "application/pdf", ".pdf" },
        { "image/png", ".png" }
    };
}