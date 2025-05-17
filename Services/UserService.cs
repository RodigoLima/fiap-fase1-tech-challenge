using fiap_fase1_tech_challenge.DTOs.Users;
using fiap_fase1_tech_challenge.Exceptions;
using fiap_fase1_tech_challenge.Messages;
using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Repositories.Interfaces;
using fiap_fase1_tech_challenge.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace fiap_fase1_tech_challenge.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IRoleService _roleService;
        private readonly IPasswordHasher _hasher;

        public UserService(IUserRepository userRepository, IRoleService roleService, IPasswordHasher hasher)
        {
            _userRepository = userRepository;
            _roleService = roleService;
            _hasher = hasher;
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _userRepository.GetAllAsync();

        public async Task<User?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                throw new NotFoundException(UserMessages.General.NotFound);

            return user;
        }
        public async Task<UserResponse> CreateAsync(UserCreateRequest user)
        {
            var role = await _roleService.GetByIdAsync(user.RoleId);

            if (role == null)
                throw new NotFoundException(RoleMessages.RoleNotFoundMessage);

            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = _hasher.Hash(user.Password),
                RoleId = role.Id
            };

            await _userRepository.CreateAsync(newUser);

            return new UserResponse
            {
                Id = newUser.Id, 
                Name = user.Name,
                Email = user.Email,
                RoleName = role.Name
            };
        }

        public async Task<bool> UpdateAsync(int id, UserUpdateRequest user)
        {
            var newUser = await GetByIdAsync(id);

            if(user.RoleId != null)
            {
                newUser!.RoleId = (int)user.RoleId;
                var role = await _roleService.GetByIdAsync(newUser.RoleId);
                if (role == null)
                    throw new NotFoundException(RoleMessages.RoleNotFoundMessage);
            }


            // Atualização de senha, se necessário
            if (!string.IsNullOrWhiteSpace(user.NewPassword))
            {
                if (string.IsNullOrWhiteSpace(user.OldPassword))
                    throw new ArgumentException("Senha antiga é obrigatória.");

                var senhaCorreta = _hasher.Verify(user.OldPassword, newUser!.Password);
                if (!senhaCorreta)
                    throw new ValidationException(UserMessages.Password.InvalidOld);

                newUser.Password = _hasher.Hash(user.NewPassword);
            }

            // Atualiza demais propriedades
            newUser!.Name = user.Name ?? newUser.Name;
            newUser!.Email = user.Email ?? newUser.Email;

            await _userRepository.UpdateAsync(newUser);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var userToDelete = await GetByIdAsync(id);

            return await _userRepository.DeleteAsync(id);
        }

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user is null || !_hasher.Verify(password, user.Password))
                throw new UnauthorizedAccessException(UserMessages.General.InvalidEmailOrPassword);

            return user;
        }

    }
}
