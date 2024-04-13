﻿using System.ComponentModel.Design;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Admission.Dictionary.Application.DTOs;
using Admission.Dictionary.Application.DTOs.Responses;
using Admission.Dictionary.Application.Services;
using Admission.Dictionary.Infrastructure.Options;
using Microsoft.Extensions.Options;

namespace Admission.Dictionary.Infrastructure.Services;

public class ExternalDictionaryService: IExternalDictionaryService
{
    private readonly HttpClient _httpClient;

    public ExternalDictionaryService(IOptions<ApiOptions> apiOptions, HttpClient httpClient)
    {
        var options = apiOptions.Value;
        _httpClient = httpClient;  
        
        _httpClient.BaseAddress = new Uri(options.BaseUrl);

        var credentials = $"{options.Username}:{options.Password}";
        var base64Credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64Credentials);
    }

    public async Task<IEnumerable<FacultyDto>> GetFacultiesAsync() =>
        (await _httpClient.GetFromJsonAsync<IEnumerable<FacultyDto>>("faculties"))!;
    
    public async Task<IEnumerable<EducationLevelDto>> GetEducationLevelsAsync() =>
        (await _httpClient.GetFromJsonAsync<IEnumerable<EducationLevelDto>>("education_levels"))!;
    
    public async Task<IEnumerable<EducationDocumentTypeDto>> GetDocumentTypesAsync() =>
        (await _httpClient.GetFromJsonAsync<IEnumerable<EducationDocumentTypeDto>>("document_types"))!;
    public async Task<ProgramPagedListDto> GetProgramsAsync(int page = 1, int size = 10) =>
        (await _httpClient.GetFromJsonAsync<ProgramPagedListDto>($"programs?page={page}&size={size}"))!;
}