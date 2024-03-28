using Microsoft.EntityFrameworkCore;

namespace CriptografiaAPI.Infra.Context
{
    public interface IApplicationContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
