using Admission.Application.Common.Mapping;
using Admission.DTOs.RpcModels.UserService.GetManagers;

namespace Admission.AdminPanel.Models.Manager;

public class ManagersViewModel: IMapFrom<ManagersResponse>
{
    public required List<ShortManagerViewModel> Managers { get; init; }
}