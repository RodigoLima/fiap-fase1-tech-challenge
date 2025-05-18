using fiap_fase1_tech_challenge.Modules.Users.DTOs.Responses;
using fiap_fase1_tech_challenge.Modules.Users.Models;

namespace fiap_fase1_tech_challenge.Modules.Users.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<UserResponse> CreateAsync(UserCreateRequest user);
        Task<bool> UpdateAsync(int id,UserUpdateRequest user);
        Task<bool> DeleteAsync(int id);
        Task<User?> AuthenticateAsync(string email, string password);
    }
}
