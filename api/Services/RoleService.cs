using fiap_fase1_tech_challenge.DTOs.Role;
using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Repositories;
using fiap_fase1_tech_challenge.Repositories.Interfaces;
using fiap_fase1_tech_challenge.Services.Interfaces;

namespace fiap_fase1_tech_challenge.Services
{
    public class RoleService: IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly ILogger<RoleService> _logger;

        public RoleService(IRoleRepository respository, ILogger<RoleService> logger)
        {
            _roleRepository = respository;
            _logger = logger;
        }
        public Task<IEnumerable<Role>> GetAllAsync() => _roleRepository.GetAllAsync();
        public Task<Role?> GetByIdAsync(int id) => _roleRepository.GetByIdAsync(id);
        public Task<Role> CreateAsync(RoleCreateRequest role)
        {
            _logger.LogInformation("Iniciando criação do Role");

            var newRole = new Role
            {
                Name = role.Name
            };

            _logger.LogInformation("Criando o Role {Role}", newRole.Name);
            return _roleRepository.CreateAsync(newRole);
        }
        
        public async Task<bool> UpdateAsync(int id,RoleUpdateRequest role)
        {

            var newRole = await _roleRepository.GetByIdAsync(id);
            if (newRole == null)
                return false;

            newRole.Name = role.Name;

            await _roleRepository.UpdateAsync(newRole);

            return true;

        }
        public Task<bool> DeleteAsync(int id) => _roleRepository.DeleteAsync(id);
    }
}
