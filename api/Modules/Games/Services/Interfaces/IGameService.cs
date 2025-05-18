using fiap_fase1_tech_challenge.Modules.Games.DTOs.Requests;
using fiap_fase1_tech_challenge.Modules.Games.DTOs.Responses;
using fiap_fase1_tech_challenge.Modules.Games.Models;


public interface IGameService
{
    Task<IEnumerable<Game>> GetAllAsync();
    Task<Game?> GetByIdAsync(int id);
    Task<GameResponse> CreateAsync(GameCreateRequest game);
    Task<bool> UpdateAsync(int id, GameUpdateRequest game);
    Task<bool> DeleteAsync(int id);
}

