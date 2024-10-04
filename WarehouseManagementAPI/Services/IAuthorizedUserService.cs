using System.Security.Claims;
using WarehouseManagementAPI.Models;

namespace WarehouseManagementAPI.Services
{
    public interface IAuthorizedUserService
    {
        ClaimsPrincipal GetAuthorizedUser();

        bool IsAuthorized();

        Guid GetCurrentPersonId();

        Guid GetCurrentDoctorId();

        string GenerateToken(User user);
    }
}
