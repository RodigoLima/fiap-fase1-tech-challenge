using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Repositories;

namespace fiap_fase1_tech_challenge.Services
{
    public class RoleService: IRoleService
    {
        private readonly IRoleRepository _repository;
        public RoleService(IRoleRepository respository)
        {
            _repository = respository;
        }
        public Task<IEnumerable<Role>> GetAllAsync() => _repository.GetAllAsync();
        public Task<Role?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<Role> CreateAsync(Role role) => _repository.CreateAsync(role);
        public Task<bool> UpdateAsync(Role role) => _repository.UpdateAsync(role);
        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);
    }
}
