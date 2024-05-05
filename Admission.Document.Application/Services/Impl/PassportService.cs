using Admission.Application.Common.Exceptions;
using Admission.Application.Common.Extensions;
using Admission.Application.Common.Result;
using Admission.Document.Application.Context;
using Admission.Document.Application.DTOs.Requests;
using Admission.Document.Application.DTOs.Responses;
using Admission.Document.Domain.Entities;
using Admission.Document.Domain.Enums;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Admission.Document.Application.Services.Impl;

public sealed class PassportService: IPassportService
{
    private readonly IDocumentDbContext _context;
    private readonly IMapper _mapper;

    public PassportService(IDocumentDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Result> CreatePassportAsync(CreatePassportDto passportDto, Guid userId)
    {
        if (_context.Passports.GetUndeleted().Any(p => p.ApplicantId == userId))
        {
            return new BadRequestException("Passport already exists");
        }

        await _context.Passports.AddAsync(new Passport
        {
            Series = passportDto.Series,
            Number = passportDto.Number,
            PlaceOfBirth = passportDto.PlaceOfBirth,
            IssuedBy = passportDto.IssuedBy,
            DateIssued = passportDto.DateIssued,
            ApplicantId = userId,
            Type = DocumentType.Passport
        });

        await _context.SaveChangesAsync();
        
        return Result.Success();
    }

    public async Task<Result<PassportDto>> GetPassportAsync(Guid userId)
    {
        var passport = await _context.Passports.GetUndeleted().FirstOrDefaultAsync(p => p.ApplicantId == userId);

        if (passport == null)
        {
            return new NotFoundException("Passport was not found");
        }

        return _mapper.Map<PassportDto>(passport);
    }

    public async Task<Result> EditPassportAsync(EditPassportDto passportDto, Guid userId)
    {
        var passport = await _context.Passports.GetUndeleted().FirstOrDefaultAsync(p => p.ApplicantId == userId);

        if (passport == null)
        {
            return new NotFoundException("Passport was not found");
        }

        passport.Series = passportDto.Series;
        passport.Number = passportDto.Number;
        passport.PlaceOfBirth = passportDto.PlaceOfBirth;
        passport.IssuedBy = passportDto.IssuedBy;
        passport.DateIssued = passportDto.DateIssued;

        await _context.SaveChangesAsync();

        return Result.Success();
    }
}