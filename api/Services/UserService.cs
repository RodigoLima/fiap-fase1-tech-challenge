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

            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = _hasher.Hash(user.Password),
                RoleId = role!.Id
            };

            var createdUser = await _userRepository.CreateAsync(newUser);

            return new UserResponse
            {
                Id = createdUser.Id, 
                Name = createdUser.Name,
                Email = createdUser.Email,
                RoleName = role.Name
            };
        }

        public string? UpdatePassword(User user, string oldPassword, string? newPassword)
        {
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                var senhaCorreta = _hasher.Verify(oldPassword, user.Password);
                if (!senhaCorreta)
                    throw new ValidationException(UserMessages.Password.InvalidOld);

               return _hasher.Hash(newPassword);
            }
            return null;
        }

        public async Task<bool> UpdateAsync(int id, UserUpdateRequest user)
        {
            var newUser = await GetByIdAsync(id);

            if (user.RoleId != null)
                await _roleService.GetByIdAsync((int)user.RoleId);

            var password = UpdatePassword(newUser!, user.OldPassword!, user.NewPassword);

            newUser!.Name = user.Name ?? newUser.Name;
            newUser!.Email = user.Email ?? newUser.Email;
            newUser!.RoleId = user.RoleId ?? newUser.RoleId;

            if(password != null)
            {
                newUser!.Password = password;
            }

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
