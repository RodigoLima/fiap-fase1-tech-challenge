using fiap_fase1_tech_challenge.DTOs.Role;
using fiap_fase1_tech_challenge.Models;

namespace fiap_fase1_tech_challenge.Services.Interfaces
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
