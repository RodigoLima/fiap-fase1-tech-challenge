using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Repositories;

namespace fiap_fase1_tech_challenge.Services
{
    public class UserService: IUserService
    {

        private readonly IUserRepository _repository;

        public UserService(IUserRepository respository)
        {
            _repository = respository;
        }

        public Task<IEnumerable<User>> GetAllAsync() => _repository.GetAllAsync();
        public Task<User?> GetByIdAsync(int id) => _repository.GetByIdAsync(id);
        public Task<User> CreateAsync(User user)
        {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                return _repository.CreateAsync(user);
        }
        public Task<bool> UpdateAsync(User user) => _repository.UpdateAsync(user);
        public Task<bool> DeleteAsync(int id) => _repository.DeleteAsync(id);

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            var user = await _repository.GetByEmailAsync(email);
            if (user is null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                await Task.Delay(200); 
                return null;
            }

            return user;
        }

    }
}
