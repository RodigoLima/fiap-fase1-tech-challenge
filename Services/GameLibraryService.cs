using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Repositories.Interfaces;
using fiap_fase1_tech_challenge.Services.Interfaces;

namespace fiap_fase1_tech_challenge.Services
{
    public class GameLibraryService : IGameLibraryService
    {

        private readonly IGameLibraryRepository _gameLibraryRepository;

        public GameLibraryService(IGameLibraryRepository gameLibraryRepository)
        {
            _gameLibraryRepository = gameLibraryRepository;
        }
        public Task<IEnumerable<GameLibrary>> GetAllAsync() => _gameLibraryRepository.GetAllAsync();
        public Task<GameLibrary?> GetByIdAsync(int id) => _gameLibraryRepository.GetByIdAsync(id);
        public Task<GameLibrary> CreateAsync(GameLibrary gameLibrary) => _gameLibraryRepository.CreateAsync(gameLibrary);
        public Task<bool> UpdateAsync(GameLibrary gameLibrary) => _gameLibraryRepository.UpdateAsync(gameLibrary);
        public Task<bool> DeleteAsync(int id) => _gameLibraryRepository.DeleteAsync(id);
    }
}
