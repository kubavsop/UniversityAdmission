namespace Admission.Dictionary.Application.Constants;

internal static class RoutingKeys
{
    internal const string FacultyChangedRoutingKey = "user.document.admission";
    internal const string ProgramChangedRoutingKey = ".admission";
    internal const string DocumentChangedRoutingKey = ".document.";
    internal const string LevelChangedRoutingKey = ".document.admission";
    internal const string NextLevelChangedRoutingKey = ".document.";
}