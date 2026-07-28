using Microsoft.EntityFrameworkCore;
using TinyLibrary.Application.Interfaces;
using TinyLibrary.Domain.Models;

namespace TinyLibrary.Infrastructure.Persistence.Repositories
{
    public class EfBookRepository : IBookRepository
    {
        private readonly LibraryDbContext context;

        public EfBookRepository(LibraryDbContext _context)
        {
            context = _context;
        }

        public void Add(Book book)
        {
            context.Books.Add(book);
        }

        public async Task<IReadOnlyCollection<Book>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await context.Books.ToListAsync(cancellationToken);
        }

        public async Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await context.Books.FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
        }

        public async Task<Book?> GetByIsbnAsync(string isbn, CancellationToken cancellationToken)
        {
            return await context.Books.FirstOrDefaultAsync(b => b.ISBN == isbn, cancellationToken);
        }
    }
}
