﻿using Admission.Domain.Common.Enums;

namespace Admission.DTOs.RpcModels.UserService.GetApplicantData;

public sealed class ApplicantDataResponse: IRpcResponse
{
    public required string Email { get; init; }
    public required string FullName { get; init; }
    public required Guid ApplicantId { get; init; }
    
    public required bool IsEditable { get; init; }
    
    public Gender? Gender { get; init; }
    public DateTime? Birthday { get; init; }
    public string? PhoneNumber { get; init; } 
    public string? Citizenship { get; init; }
}