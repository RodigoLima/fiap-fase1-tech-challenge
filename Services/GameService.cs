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
        public async Task<bool> UpdateAsync(int id,GameUpdateRequest game)
        {
            var newGame = await _gameRepository.GetByIdAsync(id);
            if (newGame == null)
                return false;

            newGame.Name = game.Name;
            newGame.Description = game.Description;
            newGame.Price = game.Price;
            if (game.ReleasedDate != null)
                newGame.ReleasedDate = game.ReleasedDate;
            newGame.Genre = game.Genre;


            await _gameRepository.UpdateAsync(newGame);

            return true;
        }
        public Task<bool> DeleteAsync(int id) => _gameRepository.DeleteAsync(id);
    }
    

}
