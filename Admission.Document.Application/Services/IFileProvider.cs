namespace Admission.Document.Application.Services;

public interface IFileProvider
{
    Task<byte[]> GetFileAsync(Guid id);
    Task PutFileAsync(Guid id, byte[] file);
}