namespace Admission.User.Application.Constants;

internal static class RoutingKeys
{
    internal const string ApplicantChangedRoutingKey = ".admission";
    internal const string ApplicantCreatedRoutingKey = "document.admission";
    internal const string ManagerCreatedRoutingKey = "document.admission";
    internal const string ManagerChangedRoutingKey = ".admission";
}