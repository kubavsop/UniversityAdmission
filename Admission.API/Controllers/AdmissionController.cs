using Admission.API.Common;
using Microsoft.AspNetCore.Mvc;

namespace Admission.API.Controllers;

public sealed class AdmissionController: BaseController
{
   [HttpPost]
   public async Task<IActionResult> CreateAdmission()
   {
      throw new NotImplementedException();
   }

   [HttpGet]
   [Route("{id:guid}")]
   public async Task<IActionResult> GetAdmissions(Guid id)
   {
      throw new NotImplementedException();
   }
}