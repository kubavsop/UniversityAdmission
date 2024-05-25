using Admission.DTOs.RpcModels.Base;

namespace Admission.DTOs.RpcModels.DocumentService.ChangePassport;

public sealed class ChangePassportRequest: AuthorizedRequest, IRpcRequest<IRpcResponse>
{
    public Guid PassportId { get; init; }
    public required int Series { get; init; }
    public required int Number { get; init; }
    public required string PlaceOfBirth { get; init; }
    public required string IssuedBy { get; init; }
    public required DateTime DateIssued 
    {
        get => _dateIssued;
        set => _dateIssued = DateTime.SpecifyKind(value, DateTimeKind.Utc);
    }
    private DateTime _dateIssued;
}