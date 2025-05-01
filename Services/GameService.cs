using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Repositories.Interfaces;
using fiap_fase1_tech_challenge.Services.Interfaces;

namespace fiap_fase1_tech_challenge.Services
{
    public class GameService: IGameService 
    {
        private readonly IGameRepository  _gameRepository;
        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }
        public Task<IEnumerable<Game>> GetAllAsync() => _gameRepository.GetAllAsync();
        public Task<Game?> GetByIdAsync(int id) => _gameRepository.GetByIdAsync(id);
        public Task<Game> CreateAsync(Game game) => _gameRepository.CreateAsync(game);
        public Task<bool> UpdateAsync(Game game) => _gameRepository.UpdateAsync(game);
        public Task<bool> DeleteAsync(int id) => _gameRepository.DeleteAsync(id);
    }
    

}
