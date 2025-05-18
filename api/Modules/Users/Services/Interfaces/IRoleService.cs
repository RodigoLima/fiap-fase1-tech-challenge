using fiap_fase1_tech_challenge.Modules.Users.DTOs.Requests;
using fiap_fase1_tech_challenge.Modules.Users.DTOs.Responses;
using fiap_fase1_tech_challenge.Modules.Users.Models;

namespace fiap_fase1_tech_challenge.Modules.Users.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllAsync();
        Task<Role?> GetByIdAsync(int id);
        Task<RoleResponse> CreateAsync(RoleCreateRequest role);
        Task<bool> UpdateAsync(int id,RoleUpdateRequest role);
        Task<bool> DeleteAsync(int id);
    }
}
