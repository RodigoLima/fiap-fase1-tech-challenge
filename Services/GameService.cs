using fiap_fase1_tech_challenge.Database;
using fiap_fase1_tech_challenge.DTOs.Game;
using fiap_fase1_tech_challenge.Enums;
using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Repositories;
using fiap_fase1_tech_challenge.Repositories.Interfaces;
using fiap_fase1_tech_challenge.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;

namespace fiap_fase1_tech_challenge.Services
{
    public class GameService : IGameService
    {

        private readonly IGameRepository _gameRepository;
        private readonly IRoleRepository _roleRepository;

        public GameService(IGameRepository GameRepository, IRoleRepository roleRepository)
        {
            _gameRepository = GameRepository;
            _roleRepository = roleRepository;
        }

        public Task<IEnumerable<Game>> GetAllAsync() => _gameRepository.GetAllAsync();
        public Task<Game?> GetByIdAsync(int id) => _gameRepository.GetByIdAsync(id);
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
            var existingGame = await _gameRepository.GetByIdAsync(id);
            if (existingGame == null)
                return false;

            existingGame.Name = game.Name;
            existingGame.Description = game.Description;
            existingGame.Price = game.Price;
            if (game.ReleasedDate != null)
                existingGame.ReleasedDate = game.ReleasedDate;
            existingGame.Genre = game.Genre;

            await _gameRepository.UpdateAsync(existingGame);

            return true;
        }
        public Task<bool> DeleteAsync(int id) => _gameRepository.DeleteAsync(id);
    }


}
