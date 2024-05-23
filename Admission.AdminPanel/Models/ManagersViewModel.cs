using Admission.Application.Common.Mapping;
using Admission.DTOs.RpcModels.UserService.GetManagers;

namespace Admission.AdminPanel.Models;

public class ManagersViewModel: IMapFrom<ManagersResponse>
{
    public required List<ShortManagerViewModel> Managers { get; init; }
}