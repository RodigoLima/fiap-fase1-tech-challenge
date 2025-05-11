using fiap_fase1_tech_challenge.DTOs.GameLibrary;
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
        public async Task<GameLibraryResponse?> GetByIdAsync(int id)
        {
            var gameLibrary = await _gameLibraryRepository.GetByIdAsync(id);
            if (gameLibrary == null)
            {
                return null;
            }

            return new GameLibraryResponse
            {
                UserId = gameLibrary.UserId,
                GameId = gameLibrary.GameId
            };
        }
        public Task<GameLibrary> CreateAsync(GameLibraryCreateRequest gameLibrary)
        {
            var newGameLibrary = new GameLibrary
            {
                UserId = gameLibrary.UserId,
                GameId = gameLibrary.GameId
            };
            return _gameLibraryRepository.CreateAsync(newGameLibrary);

        }
        public async Task<bool> UpdateAsync(int id,GameLibraryUpdateRequest gameLibrary)
        {
            var existingGameLibrary = await _gameLibraryRepository.GetByIdAsync(id);
            if (existingGameLibrary == null)
            {
                return false;
            }
            existingGameLibrary.UserId = gameLibrary.UserId;
            existingGameLibrary.GameId = gameLibrary.GameId;
            await _gameLibraryRepository.UpdateAsync(existingGameLibrary);

            return true;

        }
        public Task<bool> DeleteAsync(int id) => _gameLibraryRepository.DeleteAsync(id);
    }
}
