using fiap_fase1_tech_challenge.Common.Exceptions;
using fiap_fase1_tech_challenge.Modules.Games.DTOs.Requests;
using fiap_fase1_tech_challenge.Modules.Games.DTOs.Responses;
using fiap_fase1_tech_challenge.Modules.Games.Messages;
using fiap_fase1_tech_challenge.Modules.Games.Models;
using fiap_fase1_tech_challenge.Modules.Games.Repositories.Interfaces;

namespace fiap_fase1_tech_challenge.Modules.Games.Services.Implementations
{
    public class GameService : IGameService
    {

        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository GameRepository)
        {
            _gameRepository = GameRepository;
        }

        public async Task<IEnumerable<Game>> GetAllAsync() => await _gameRepository.GetAllAsync();
        public async Task<Game?> GetByIdAsync(int id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            if (game == null)
                throw new NotFoundException(GameMessages.General.NotFound);

            return game;
        }
        public async Task<GameResponse> CreateAsync(GameCreateRequest request)
        {
            var newGame = new Game
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                ReleasedDate = request.ReleasedDate,
                Genre = request.Genre
            };

            var createdGame = await _gameRepository.CreateAsync(newGame);

            return new GameResponse
            {
                Id = createdGame.Id,
                Name = createdGame.Name,
                Description = createdGame.Description,
                Price = createdGame.Price,
                ReleasedDate = createdGame.ReleasedDate,
                Genre = createdGame.Genre
            };
        }
   
        public async Task<bool> UpdateAsync(int id, GameUpdateRequest game)
        {
            var existingGame = await GetByIdAsync(id);

            existingGame!.Name = game.Name ?? existingGame.Name;
            existingGame!.Description = game.Description ?? existingGame.Description;
            existingGame!.Price = game.Price ?? existingGame.Price;
            existingGame!.ReleasedDate = game.ReleasedDate ?? existingGame.ReleasedDate;
            existingGame!.Genre = game.Genre ?? existingGame.Genre;

            return await _gameRepository.UpdateAsync(existingGame);
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var game = await GetByIdAsync(id);

            return await _gameRepository.DeleteAsync(id);
        }
    }
}
