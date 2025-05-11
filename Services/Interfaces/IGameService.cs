using fiap_fase1_tech_challenge.DTOs.Game;
using fiap_fase1_tech_challenge.Models;


public interface IGameService
{
    Task<IEnumerable<Game>> GetAllAsync();
    Task<Game?> GetByIdAsync(int id);
    Task<GameResponse> CreateAsync(GameCreateRequest game);
    Task<bool> UpdateAsync(int id, GameUpdateRequest game);
    Task<bool> DeleteAsync(int id);
}

