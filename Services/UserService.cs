using fiap_fase1_tech_challenge.Database;
using fiap_fase1_tech_challenge.DTOs.Users;
using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Repositories;
using fiap_fase1_tech_challenge.Repositories.Interfaces;
using fiap_fase1_tech_challenge.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace fiap_fase1_tech_challenge.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IPasswordHasher _hasher;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository, IPasswordHasher hasher)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _hasher = hasher;
        }

        public Task<IEnumerable<User>> GetAllAsync() => _userRepository.GetAllAsync();
        public Task<User?> GetByIdAsync(int id) => _userRepository.GetByIdAsync(id);
        public async Task<UserResponse> CreateAsync(UserCreateRequest user)
        {
            var role = await _roleRepository.GetByIdAsync(user.RoleId);

            if (role == null)
                throw new ArgumentException($"Role com ID {user.RoleId} não encontrado.");

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
            var newUser = await _userRepository.GetByIdAsync(id);
            if (newUser == null)
                return false;

            var role = await _roleRepository.GetByIdAsync(user.RoleId);
            if (role == null)
                throw new ArgumentException($"Role com ID {user.RoleId} não encontrado.");

            // Atualização de senha, se necessário
            if (!string.IsNullOrWhiteSpace(user.NewPassword))
            {
                if (string.IsNullOrWhiteSpace(user.OldPassword))
                    throw new ArgumentException("Senha antiga é obrigatória.");

                var senhaCorreta = _hasher.Verify(user.OldPassword, newUser.Password);
                if (!senhaCorreta)
                    throw new UnauthorizedAccessException("Senha antiga incorreta.");

                newUser.Password = _hasher.Hash(user.NewPassword);
            }

            // Atualiza demais propriedades
            newUser.Name = user.Name;
            newUser.Email = user.Email;
            newUser.RoleId = role.Id;

            await _userRepository.UpdateAsync(newUser);
            return true;
        }

        public Task<bool> DeleteAsync(int id) => _userRepository.DeleteAsync(id);

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                await Task.Delay(200);
                return null;
            }

            return user;
        }

    }
}
