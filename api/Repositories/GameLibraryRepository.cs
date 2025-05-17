using fiap_fase1_tech_challenge.Database;
using fiap_fase1_tech_challenge.Models;
using fiap_fase1_tech_challenge.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fiap_fase1_tech_challenge.Repositories
{
    public class GameLibraryRepository : IGameLibraryRepository
    {
        private readonly ApplicationContext _context;

        public GameLibraryRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GameLibrary>> GetAllAsync() => await _context.GameLibraries.ToListAsync();
        public async Task<IEnumerable<GameLibrary>> GetAllByUser(int userId) => await _context.GameLibraries.Include(gl => gl.Game).Where(gl => gl.UserId == userId).ToListAsync();

        public async Task<GameLibrary?> GetByIdAsync(int id) => await _context.GameLibraries.Include(gl => gl.Game).FirstOrDefaultAsync(gl => gl.Id == id);

        public async Task<GameLibrary> CreateAsync(GameLibrary gameLibrary)
        {
            _context.GameLibraries.Add(gameLibrary);
            await _context.SaveChangesAsync();
            return gameLibrary;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var glToDelete = await _context.GameLibraries.FirstOrDefaultAsync(gl => gl.Id == id);
            if (glToDelete == null) return false;
            _context.GameLibraries.Remove(glToDelete);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(GameLibrary gameLibrary)
        {
            _context.GameLibraries.Update(gameLibrary);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
