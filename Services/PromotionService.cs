using fiap_fase1_tech_challenge.Database;
using fiap_fase1_tech_challenge.DTOs.Promotion;
using fiap_fase1_tech_challenge.Enums;
using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Repositories;
using fiap_fase1_tech_challenge.Repositories.Interfaces;
using fiap_fase1_tech_challenge.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;

namespace fiap_fase1_tech_challenge.Services
{
  public class PromotionService : IPromotionService
  {

    private readonly IPromotionRepository _PromotionRepository;
    private readonly IRoleRepository _roleRepository;

    public PromotionService(IPromotionRepository PromotionRepository, IRoleRepository roleRepository)
    {
      _PromotionRepository = PromotionRepository;
      _roleRepository = roleRepository;
    }

    public Task<IEnumerable<Promotion>> GetAllAsync() => _PromotionRepository.GetAllAsync();
    public Task<Promotion?> GetByIdAsync(int id) => _PromotionRepository.GetByIdAsync(id);
    public async Task<PromotionResponse> CreateAsync(PromotionCreateRequest request)
    {
      await _validarRole(request);

      var Promotion = new Promotion
      {
        DiscountPercentage = request.DiscountPercentage,
        InitialDate = request.InitialDate,
        FinalDate = request.FinalDate,
        GameId = request.GameId
      };

      await _PromotionRepository.CreateAsync(Promotion);

      return new PromotionResponse
      {
        Id = Promotion.Id,
        DiscountPercentage = request.DiscountPercentage,
        InitialDate = request.InitialDate,
        FinalDate = request.FinalDate,
        GameId = request.GameId
      };

    }

    public Task<bool> UpdateAsync(Promotion Promotion) => _PromotionRepository.UpdateAsync(Promotion);
    public Task<bool> DeleteAsync(int id) => _PromotionRepository.DeleteAsync(id);

    private async Task _validarRole(PromotionCreateRequest request)
    {
      Role role = await _roleRepository.GetByIdAsync(request.RoleId);

      if (role == null)
        throw new ArgumentException($"Role com ID {request.RoleId} não encontrado.");
      else if (role.Id == (int)ERole.Admin)
        throw new AuthenticationException($"Role com ID {request.RoleId} não tem permissão para executar esta ação.");
    }
  }
}
