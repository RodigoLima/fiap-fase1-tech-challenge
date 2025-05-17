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

        private readonly IPromotionRepository _promotionRepository;
        private readonly IRoleRepository _roleRepository;

        public PromotionService(IPromotionRepository promotionRepository, IRoleRepository roleRepository)
        {
            _promotionRepository = promotionRepository;
            _roleRepository = roleRepository;
        }

        public Task<IEnumerable<Promotion>> GetAllAsync() => _promotionRepository.GetAllAsync();
        public Task<Promotion?> GetByIdAsync(int id) => _promotionRepository.GetByIdAsync(id);
        public async Task<PromotionResponse> CreateAsync(PromotionCreateRequest promotion)
        {

            var newPromotion = new Promotion
            {
                DiscountPercentage = promotion.DiscountPercentage,
                InitialDate = promotion.InitialDate,
                FinalDate = promotion.FinalDate,
                GameId = promotion.GameId
            };

            var createdPromotion = await _promotionRepository.CreateAsync(newPromotion);

            return new PromotionResponse
            {
                Id = createdPromotion.Id,
                DiscountPercentage = createdPromotion.DiscountPercentage,
                InitialDate = createdPromotion.InitialDate,
                FinalDate = createdPromotion.FinalDate,
                GameId = createdPromotion.GameId
            };
        }
        public async Task<bool> UpdateAsync(int id, PromotionUpdateRequest promotion)
        {

            var existingPromotion = await _promotionRepository.GetByIdAsync(id);
            if (existingPromotion == null)
                return false;

            existingPromotion.DiscountPercentage = promotion.DiscountPercentage;
            existingPromotion.InitialDate = promotion.InitialDate;
            existingPromotion.FinalDate = promotion.FinalDate;
            existingPromotion.GameId = promotion.GameId;

            await _promotionRepository.UpdateAsync(existingPromotion);

            return true;
        }
        public Task<bool> DeleteAsync(int id) => _promotionRepository.DeleteAsync(id);

    }
}
