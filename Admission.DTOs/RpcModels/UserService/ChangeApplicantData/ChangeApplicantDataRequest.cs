using System.ComponentModel.DataAnnotations;
using Admission.Domain.Common.Enums;
using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.UserService.ChangeApplicantData;

public sealed class ChangeApplicantDataRequest: AuthorizedRequest, IRpcRequest<IRpcResponse>
{
    public required Guid ApplicantId { get; init; }
    public required string FullName { get; init; }
    public required string Email { get; init; }
    public DateTime? Birthday     
    {
        get => _birthday;
        set => _birthday = value.HasValue ? DateTime.SpecifyKind(value.Value, DateTimeKind.Utc) : value;
    }
    public Gender? Gender { get; init; }
    public string? Citizenship { get; init; }
    public string? PhoneNumber { get; init; }
    
    private DateTime? _birthday;
}