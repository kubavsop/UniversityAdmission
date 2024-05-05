using Admission.Application.Common.Mapping;
using File = Admission.Document.Domain.Entities.File;

namespace Admission.Document.Application.DTOs.Responses;

public sealed class FileDto: IMapFrom<File>
{
    public required string Name { get; set; }
    public int Size { get; set; }
}