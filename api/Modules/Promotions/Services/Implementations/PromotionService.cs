using fiap_fase1_tech_challenge.Common.Exceptions;
using fiap_fase1_tech_challenge.Modules.Promotions.DTOs.Requests;
using fiap_fase1_tech_challenge.Modules.Promotions.DTOs.Responses;
using fiap_fase1_tech_challenge.Modules.Promotions.Messages;
using fiap_fase1_tech_challenge.Modules.Promotions.Models;
using fiap_fase1_tech_challenge.Modules.Promotions.Repositories.Interfaces;
using fiap_fase1_tech_challenge.Modules.Promotions.Services.Interfaces;

namespace fiap_fase1_tech_challenge.Modules.Promotions.Services.Implementations
{
    public class PromotionService : IPromotionService
    {

        private readonly IPromotionRepository _promotionRepository;
        private readonly IGameService _gameService;

        public PromotionService(IPromotionRepository promotionRepository, IGameService gameService)
        {
            _promotionRepository = promotionRepository;
            _gameService = gameService;
        }

        public Task<IEnumerable<Promotion>> GetAllAsync() => _promotionRepository.GetAllAsync();
        public async Task<Promotion?> GetByIdAsync(int id)
        {
            var promotion = await _promotionRepository.GetByIdAsync(id);
            if (promotion is null)
                throw new NotFoundException(PromotionMessages.General.NotFound);

            return promotion;
        }
        public async Task<PromotionResponse> CreateAsync(PromotionCreateRequest promotion)
        {
            await _gameService.GetByIdAsync(promotion.GameId);

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
            var existingPromotion = await GetByIdAsync(id);

            if(promotion.GameId != null)
            {
                await _gameService.GetByIdAsync((int)promotion.GameId);
            }

            existingPromotion!.DiscountPercentage = promotion.DiscountPercentage ?? existingPromotion.DiscountPercentage;
            existingPromotion!.InitialDate = promotion.InitialDate ?? existingPromotion.InitialDate;
            existingPromotion!.FinalDate = promotion.FinalDate ?? existingPromotion.FinalDate;
            existingPromotion!.GameId = promotion.GameId ?? existingPromotion.GameId;

            await _promotionRepository.UpdateAsync(existingPromotion);

            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var promotion = await GetByIdAsync(id);

            return await _promotionRepository.DeleteAsync(id);
        }

    }
}
