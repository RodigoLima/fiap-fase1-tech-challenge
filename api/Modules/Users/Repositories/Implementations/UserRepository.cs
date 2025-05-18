using fiap_fase1_tech_challenge.Database;
using fiap_fase1_tech_challenge.Modules.Users.Models;
using fiap_fase1_tech_challenge.Modules.Users.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace fiap_fase1_tech_challenge.Modules.Users.Repositories.Implementations
{
    public class UserRepository: IUserRepository
    {

        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _context.Users.ToListAsync();
        public async Task<User?> GetByIdAsync(int id) => await _context.Users.FirstOrDefaultAsync(U => U.Id == id);
        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(U => U.Id == id);
            if (user == null) return false;
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);
        }



    }
}
