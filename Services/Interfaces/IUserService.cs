using fiap_fase1_tech_challenge.DTOs.Users;
using fiap_fase1_tech_challenge.Models;

namespace fiap_fase1_tech_challenge.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<UserResponse> CreateAsync(UserCreateRequest user);
        Task<bool> UpdateAsync(User user);
        Task<bool> DeleteAsync(int id);
        Task<User?> AuthenticateAsync(string email, string password);
    }
}
