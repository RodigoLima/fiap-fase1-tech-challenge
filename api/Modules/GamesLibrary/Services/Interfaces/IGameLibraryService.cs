using fiap_fase1_tech_challenge.Modules.GamesLibrary.DTOs.Requests;
using fiap_fase1_tech_challenge.Modules.GamesLibrary.DTOs.Responses;
using fiap_fase1_tech_challenge.Modules.GamesLibrary.Models;

namespace fiap_fase1_tech_challenge.Modules.GamesLibrary.Services.Interfaces
{
    public interface IGameLibraryService
    {
        Task<IEnumerable<GameLibrary>> GetAllAsync();
        Task<GameLibraryResponse?> GetByIdAsync(int id);
        Task<GameLibrary> CreateAsync(GameLibraryCreateRequest gameLibrary);
        Task<bool> UpdateAsync(int id,GameLibraryUpdateRequest gameLibrary);
        Task<bool> DeleteAsync(int id);
    }
}
