using TinyLibrary.Domain.Models;

namespace TinyLibrary.Application.Interfaces
{
    public interface IBookRepository
    {
        Task<Book?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Book?> GetByIsbnAsync(string isbn, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Book>> GetAllAsync(CancellationToken cancellationToken);
        void Add(Book book);

    }
}
