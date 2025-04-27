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

    private readonly IGameRepository _GameRepository;
    private readonly IRoleRepository _roleRepository;

    public GameService(IGameRepository GameRepository, IRoleRepository roleRepository)
    {
      _GameRepository = GameRepository;
      _roleRepository = roleRepository;
    }

    public Task<IEnumerable<Game>> GetAllAsync() => _GameRepository.GetAllAsync();
    public Task<Game?> GetByIdAsync(int id) => _GameRepository.GetByIdAsync(id);
    public async Task<GameResponse> CreateAsync(GameCreateRequest request)
    {

      var role = await _roleRepository.GetByIdAsync(request.RoleId);

      if (role == null)
        throw new ArgumentException($"Role com ID {request.RoleId} não encontrado.");
      else if (role.Id == (int)ERole.Admin)
        throw new AuthenticationException($"Role com ID {request.RoleId} não tem permissão para executar esta ação.");


      var Game = new Game
      {
        Name = request.Name,
        Description = request.Description,
        Price = request.Price,
        ReleasedDate = request.ReleasedDate,
        Genre = request.Genre
      };

      await _GameRepository.CreateAsync(Game);

      return new GameResponse
      {
        Id = Game.Id,
        Name = request.Name,
        Description = request.Description,
        Price = request.Price,
        ReleasedDate = request.ReleasedDate,
        Genre = request.Genre
      };

    }
    public Task<bool> UpdateAsync(Game Game) => _GameRepository.UpdateAsync(Game);
    public Task<bool> DeleteAsync(int id) => _GameRepository.DeleteAsync(id);
  }
}
