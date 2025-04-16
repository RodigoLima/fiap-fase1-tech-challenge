using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Repositories.Interfaces;
using fiap_fase1_tech_challenge.Services.Interfaces;

namespace fiap_fase1_tech_challenge.Services
{
    public class RoleService: IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository respository)
        {
            _roleRepository = respository;
        }
        public Task<IEnumerable<Role>> GetAllAsync() => _roleRepository.GetAllAsync();
        public Task<Role?> GetByIdAsync(int id) => _roleRepository.GetByIdAsync(id);
        public Task<Role> CreateAsync(Role role) => _roleRepository.CreateAsync(role);
        public Task<bool> UpdateAsync(Role role) => _roleRepository.UpdateAsync(role);
        public Task<bool> DeleteAsync(int id) => _roleRepository.DeleteAsync(id);
    }
}
