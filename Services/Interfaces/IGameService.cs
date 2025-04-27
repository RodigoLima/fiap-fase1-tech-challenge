using fiap_fase1_tech_challenge.DTOs.Game;
using fiap_fase1_tech_challenge.Models;

namespace fiap_fase1_tech_challenge.Services.Interfaces
{
  public interface IGameService
  {
    Task<IEnumerable<Game>> GetAllAsync();
    Task<Game?> GetByIdAsync(int id);
    Task<GameResponse> CreateAsync(GameCreateRequest Game);
    Task<bool> UpdateAsync(Game Game);
    Task<bool> DeleteAsync(int id);
  }
}
