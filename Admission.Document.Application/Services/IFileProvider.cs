namespace Admission.Document.Application.Services;

public interface IFileProvider
{
    Task<Stream> GetFile(Guid id);
    Task PutFile(Guid id, Stream file);
}