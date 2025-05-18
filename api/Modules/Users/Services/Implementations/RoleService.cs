using fiap_fase1_tech_challenge.Common.Exceptions;
using fiap_fase1_tech_challenge.Modules.Users.DTOs.Requests;
using fiap_fase1_tech_challenge.Modules.Users.DTOs.Responses;
using fiap_fase1_tech_challenge.Modules.Users.Messages;
using fiap_fase1_tech_challenge.Modules.Users.Models;
using fiap_fase1_tech_challenge.Modules.Users.Repositories.Interfaces;
using fiap_fase1_tech_challenge.Modules.Users.Services.Interfaces;

namespace fiap_fase1_tech_challenge.Modules.Users.Services.Implementations
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
        public async Task<Role?> GetByIdAsync(int id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role is null)
                throw new NotFoundException(RoleMessages.General.NotFound);

            return role;
        }
        public async Task<RoleResponse> CreateAsync(RoleCreateRequest role)
        {
            _logger.LogInformation("Iniciando criação do Role");

            var newRole = new Role
            {
                Name = role.Name
            };

            _logger.LogInformation("Criando o Role {Role}", newRole.Name);
            
            var createdRole = await _roleRepository.CreateAsync(newRole);

            return new RoleResponse
            {
                Id = createdRole.Id,
                Name = createdRole.Name
            };
        }
        
        public async Task<bool> UpdateAsync(int id,RoleUpdateRequest role)
        {
            if(role.Name != null)
            {
                var newRole = await GetByIdAsync(id);

                newRole!.Name = role.Name;

                await _roleRepository.UpdateAsync(newRole);
            }

            return true;

        }
        public async Task<bool> DeleteAsync(int id)
        {
            var role = await GetByIdAsync(id);

            return await _roleRepository.DeleteAsync(id);
        }
    }
}
