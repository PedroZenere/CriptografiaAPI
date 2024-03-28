using CriptografiaAPI.Infra.Context;

namespace CriptografiaAPI.Infra.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationContext _context;

        public UnitOfWork(IApplicationContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
    }
}
