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
        public async Task<UserResponse> CreateAsync(UserCreateRequest request)
        {

            var role = await _roleRepository.GetByIdAsync(request.RoleId);

            if (role == null)
                throw new ArgumentException($"Role com ID {request.RoleId} não encontrado.");

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = _hasher.Hash(request.Password),
                RoleId = role.Id
            };

            await _userRepository.CreateAsync(user);

            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                RoleName = role.Name
            };

        }

        public async Task<bool> UpdateAsync(int id, UserUpdateRequest request)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return false;

            var role = await _roleRepository.GetByIdAsync(request.RoleId);
            if (role == null)
                throw new ArgumentException($"Role com ID {request.RoleId} não encontrado.");

            // Atualização de senha, se necessário
            if (!string.IsNullOrWhiteSpace(request.NewPassword))
            {
                if (string.IsNullOrWhiteSpace(request.OldPassword))
                    throw new ArgumentException("Senha antiga é obrigatória.");

                var senhaCorreta = _hasher.Verify(request.OldPassword, user.Password);
                if (!senhaCorreta)
                    throw new UnauthorizedAccessException("Senha antiga incorreta.");

                user.Password = _hasher.Hash(request.NewPassword);
            }

            // Atualiza demais propriedades
            user.Name = request.Name;
            user.Email = request.Email;
            user.RoleId = role.Id;

            await _userRepository.UpdateAsync(user);
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
