using Admission.Document.Domain.Events.File;
using Admission.Domain.Common.Entities;

namespace Admission.Document.Domain.Entities;

public sealed class File: AggregateRoot
{
    public Guid DocumentId { get; set; }
    public Document Document { get; set; } = null!;
    public required string Extension { get; set; }
    public required string Name { get; set; }
    public long Size { get; set; }

    private File()
    {
    }

    public static File Create( 
        Guid fileId,
        Guid documentId,
        string name,
        string extension,
        long size,
        Guid userId)
    {
        var file = new File
        {
            Id = fileId,
            DocumentId = documentId,
            Name = name,
            Extension = extension,
            Size = size
        };
        
        file.AddDomainEvent(new FileChangedDomainEvent(userId));

        return file;
    }

    public void ChangeDeleteTime(DateTime? deleteTime, Guid userId)
    {
        if (DeleteTime == deleteTime) return;
        DeleteTime = deleteTime;
        AddDomainEvent(new FileChangedDomainEvent(userId));
    }
}