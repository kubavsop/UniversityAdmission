namespace Admission.Document.Application.Constants;

internal static class ContentTypeMappings
{
    internal static Dictionary<string, string> TypeMappings { get; } = new Dictionary<string, string>
    {
        { ".jpe", "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".jpg", "image/jpeg" },
        { ".pdf", "application/pdf" },
        { ".png", "image/png" }
    };
}