using Admission.Application.Common.Exceptions;
using Admission.Application.Common.Extensions;
using Admission.Application.Common.Result;
using Admission.Document.Application.Context;
using Admission.Document.Application.DTOs.Responses;
using Microsoft.EntityFrameworkCore;
using File = Admission.Document.Domain.Entities.File;

namespace Admission.Document.Application.Services.Impl;

public sealed class ScanService: IScanService
{
    private readonly IDocumentDbContext _context;
    private readonly IFileProvider _fileProvider;
    
    public ScanService(IDocumentDbContext context, IFileProvider fileProvider)
    {
        _context = context;
        _fileProvider = fileProvider;
    }

    public async Task<Result<FileResponse>> GetScanAsync(Guid userId, Guid fileId)
    {
        var fileResult = await GetFileAsync(fileId, userId);

        if (fileResult.IsFailure) return fileResult.Exception;

        var file = fileResult.Value;

        var bytes = await _fileProvider.GetFileAsync(fileId);

        return new FileResponse
        {
            Bytes = await _fileProvider.GetFileAsync(fileId),
            Extension = file.Extension,
            Name = file.Name
        };
    }

    public async Task<Result> DeleteScanAsync(Guid userId, Guid fileId)
    {
        var fileResult = await GetFileAsync(fileId, userId);

        if (fileResult.IsFailure) return fileResult.Exception;

        var file = fileResult.Value;
        
        file.ChangeDeleteTime(DateTime.UtcNow, file.Document.ApplicantId);

        await _context.SaveChangesAsync();

        return Result.Success();
    }

    private async Task<Result<File>> GetFileAsync(Guid fileId, Guid userId)
    {
        var file = await _context.Files
            .Include(f => f.Document)
            .GetByIdAsync(fileId);
        
        if (file == null)
        {
            return new NotFoundException(nameof(File), fileId);
        }

        if (file.Document.ApplicantId != userId)
        {
            return new ForbiddenException(userId);
        }

        return file;
    }
}