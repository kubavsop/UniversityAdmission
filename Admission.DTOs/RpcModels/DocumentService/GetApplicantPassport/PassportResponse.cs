namespace Admission.DTOs.RpcModels.DocumentService.GetApplicantPassport;

public sealed class PassportResponse: IRpcResponse
{
    public Guid PassportId { get; init; }
    public required int Series { get; init; }
    public required int Number { get; init; }
    public required string PlaceOfBirth { get; init; }
    public required string IssuedBy { get; init; }
    public required DateTime DateIssued { get; init; }
    public required IEnumerable<ScanRpcModel> Scans { get; init; }
}