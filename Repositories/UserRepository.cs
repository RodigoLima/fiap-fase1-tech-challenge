using fiap_fase1_tech_challenge.Database;
using fiap_fase1_tech_challenge.Models;
using Microsoft.EntityFrameworkCore;

namespace fiap_fase1_tech_challenge.Repositories
{
    public class UserRepository
    {

        private readonly ApplicationContext _context;


        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }


    }
}
