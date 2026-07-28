using TinyLibrary.Application.Interfaces;

namespace TinyLibrary.Infrastructure.Persistence.Repositories
{
    public class EfUnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbContext context;

        public EfUnitOfWork(LibraryDbContext _context)
        {
            context = _context;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            return context.SaveChangesAsync(cancellationToken);
        }
    }
}
