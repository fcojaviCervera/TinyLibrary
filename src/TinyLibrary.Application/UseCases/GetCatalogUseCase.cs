using TinyLibrary.Application.Dtos;
using TinyLibrary.Application.Interfaces;

namespace TinyLibrary.Application.UseCases
{
    public class GetCatalogUseCase
    {
        public IBookRepository bookRepository;

        public GetCatalogUseCase(IBookRepository _bookRepository)
        {
            bookRepository = _bookRepository;
        }

        public async Task<IReadOnlyCollection<BookDto>> ExecuteAsync(CancellationToken cancellationToken)
        {
            var books = await bookRepository.GetAllAsync(cancellationToken);

            return books.Select(b => new BookDto(b.Id, b.Title, b.Author, b.ISBN, b.TotalCopies, b.AvailableCopies)).ToList();
        }
    }
}
