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
        public Task<User?> GetByIdAsync(Guid id) => _repository.GetByIdAsync(id);
        public Task<User> CreateAsync(User user) => _repository.CreateAsync(user);
        public Task<bool> UpdateAsync(User user) => _repository.UpdateAsync(user);
        public Task<bool> DeleteAsync(Guid id) => _repository.DeleteAsync(id);

    }
}
