using Admission.Dictionary.Application.DTOs;
using Admission.Dictionary.Application.DTOs.Responses;

namespace Admission.Dictionary.Application.Services;

public interface IImporterService
{
    Task TestUpdate();
    Task UpdateFaculties();
    Task UpdatePrograms();
    Task UpdateEducationLevels();
    Task UpdateDocumentTypes();
}