using fiap_fase1_tech_challenge.Database;
using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fiap_fase1_tech_challenge.Repositories
{
    public class PromotionRepository : IPromotionRepository
    {
        private readonly ApplicationContext _context;

        public PromotionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Promotion>> GetAllAsync() => await _context.Promotions.ToListAsync();

        public async Task<IEnumerable<Promotion>> GetByGame(int gameId) => await _context.Promotions.Where(p => p.GameId == gameId).ToListAsync();

        public async Task<Promotion?> GetByIdAsync(int id) => await _context.Promotions.FirstOrDefaultAsync(p => p.Id == id);

        public async Task<Promotion> CreateAsync(Promotion promotion)
        {
            _context.Promotions.Add(promotion);
            await _context.SaveChangesAsync();
            return promotion;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var promotionToDelete = await _context.Promotions.FirstOrDefaultAsync(p => p.Id == id);
            if (promotionToDelete == null) return false;
            _context.Promotions.Remove(promotionToDelete);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Promotion promotion)
        {
            _context.Promotions.Update(promotion);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
