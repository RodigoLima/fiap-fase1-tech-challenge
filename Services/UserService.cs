using fiap_fase1_tech_challenge.Database;
using fiap_fase1_tech_challenge.DTOs.Users;
using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Repositories;
using fiap_fase1_tech_challenge.Repositories.Interfaces;
using fiap_fase1_tech_challenge.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fiap_fase1_tech_challenge.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
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
                Password = HashPassword(request.Password),
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

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);

        }
        public Task<bool> UpdateAsync(User user) => _userRepository.UpdateAsync(user);
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
