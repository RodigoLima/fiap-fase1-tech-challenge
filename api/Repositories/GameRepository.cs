using fiap_fase1_tech_challenge.Database;
using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fiap_fase1_tech_challenge.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationContext _context;

        public GameRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Game>> GetAllAsync() => await _context.Games.ToListAsync();

        public async Task<Game?> GetByIdAsync(int id) => await _context.Games.FirstOrDefaultAsync(g => g.Id == id);

        public async Task<Game> CreateAsync(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
            return game;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var gameToDelete = await _context.Games.FirstOrDefaultAsync(g => g.Id == id);
            if (gameToDelete == null) return false;
            _context.Games.Remove(gameToDelete);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Game game)
        {
            _context.Games.Update(game);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
