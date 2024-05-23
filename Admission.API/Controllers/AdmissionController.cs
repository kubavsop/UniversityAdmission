using Admission.API.Common;
using Admission.API.Common.Extensions;
using Admission.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admission.API.Controllers;

[Authorize]
public sealed class AdmissionController: BaseController
{
   private readonly IAdmissionService _admissionService;

   public AdmissionController(IAdmissionService admissionService)
   {
      _admissionService = admissionService;
   }

   [HttpPost]
   public async Task<IActionResult> CreateAdmission()
   {
      var result = await _admissionService.CreateAdmissionAsync(UserId);
      return result.ToIActionResult();
   }

   [HttpGet]
   [Route("{id:guid}")]
   public async Task<IActionResult> GetAdmission(Guid id)
   {
      var result = await _admissionService.GetAdmissionAsync(id, UserId);
      return result.ToIActionResult();
   }
}