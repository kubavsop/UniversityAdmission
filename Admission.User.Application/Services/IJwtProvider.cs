using Admission.User.Domain.Entities;

namespace Admission.User.Application.Services;

public interface IJwtProvider
{
    string Generate(AdmissionUser user, IEnumerable<string> roles);
}