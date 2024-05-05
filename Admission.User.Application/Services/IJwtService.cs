using Admission.Application.Common.Result;
using Admission.User.Application.Models;
using Admission.User.Domain.Entities;

namespace Admission.User.Application.Services;

public interface IJwtService
{
    string Generate(AdmissionUser user, IEnumerable<string> roles, Guid tokenId);

    Result<TokenIdentifiers> GetIdentifiersFromToken(string token);
}