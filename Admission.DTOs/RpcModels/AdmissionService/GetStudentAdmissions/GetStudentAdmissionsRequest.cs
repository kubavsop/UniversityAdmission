﻿using Admission.Domain.Common.Enums;
using Admission.DTOs.IntegrationEvents.Events.Faculty;
using Admission.DTOs.RpcModels.Base;
using Admission.DTOs.RpcModels.Enums;

namespace Admission.DTOs.RpcModels.AdmissionService.GetStudentAdmissions;

public sealed class GetStudentAdmissionsRequest: AuthorizedRequest, IRpcRequest<StudentAdmissionsResponse>
{
    public string? EducationProgramName { get; init; }
    public string? ApplicantName { get; init; }
    public AdmissionStatus? AdmissionStatus { get; init; }
    public SortingOptions SortingOptions { get; init; }
    public bool? WithoutManager { get; init; }
    public bool OnlyMine { get; init; } = false;
    public List<Guid> Faculties { get; init; } = new List<Guid>();
    public required int Page { get; init; }
    public required int Size { get; init; }
}