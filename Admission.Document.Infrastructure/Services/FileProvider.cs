using Admission.Document.Application.Services;
using Microsoft.Extensions.Options;
using FileOptions = Admission.Document.Infrastructure.Options.FileOptions;

namespace Admission.Document.Infrastructure.Services;

public sealed class FileProvider: IFileProvider
{
    private readonly FileOptions _fileOptions;

    public FileProvider(IOptions<FileOptions> fileOptions)
    {
        _fileOptions = fileOptions.Value;
    }

    public async Task<byte[]> GetFileAsync(Guid id)
    {
        var path = Path.Combine(_fileOptions.Path, id.ToString());
        if (File.Exists(path))
        {
            return await File.ReadAllBytesAsync(path);
        }

        return null;
    }

    public async Task PutFileAsync(Guid id, byte[] file)
    {
        Directory.CreateDirectory(_fileOptions.Path);
        var path = Path.Combine(_fileOptions.Path, id.ToString());
        
        using (var fileStream = new FileStream(path, FileMode.Create, FileAccess.Write))
        {
            await fileStream.WriteAsync(file);
        }
    }
}