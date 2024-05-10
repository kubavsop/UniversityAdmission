namespace Admission.Document.Application.DTOs.Responses;

public class FileResponse
{
    public required string Name { get; init; }
    public required string Extension { get; init; }
    public required byte[] Bytes { get; init; }
}