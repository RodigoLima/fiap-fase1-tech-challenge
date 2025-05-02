using fiap_fase1_tech_challenge.DTOs.Game;
using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Repositories;
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
        public Task<Game> CreateAsync(GameCreateRequest game)
        {
            var newGame = new Game
            {
                Name = game.Name,
                Description = game.Description,
                Price = game.Price,
                ReleasedDate = game.ReleasedDate,
                Genre = game.Genre
            };
            return _gameRepository.CreateAsync(newGame);
        }
        public async Task<bool> UpdateAsync(int id,GameUpdateRequest request)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            if (game == null)
                return false;

            game.Name = request.Name;
            game.Description = request.Description;
            game.Price = request.Price;
            if (request.ReleasedDate != null)
                game.ReleasedDate = request.ReleasedDate;
            game.Genre = request.Genre;


            await _gameRepository.UpdateAsync(game);

            return true;
        }
        public Task<bool> DeleteAsync(int id) => _gameRepository.DeleteAsync(id);
    }
    

}
