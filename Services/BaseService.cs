using Microsoft.EntityFrameworkCore;

namespace fiap_fase1_tech_challenge.Services
{
    public class BaseService
    {

        protected async Task<T> RequireEntityAsync<T>(
    DbSet<T> dbSet,
    int id,
    string name,
    CancellationToken cancellationToken = default
) where T : class
        {
            var entity = await dbSet.FindAsync(new object[] { id }, cancellationToken);
            if (entity == null)
                throw new ArgumentException($"{name} com ID {id} não encontrado.");
            return entity;
        }
    }
}
