using TinyLibrary.Application.Dtos;
using TinyLibrary.Application.Exceptions;
using TinyLibrary.Application.Interfaces;
using TinyLibrary.Domain.Models;

namespace TinyLibrary.Application.UseCases
{
    public class RegisterBookUseCase
    {

        private IUnitOfWork unitOfWork;
        private IBookRepository bookRepository;

        public RegisterBookUseCase(IUnitOfWork _unitOfWork, IBookRepository _bookRepository)
        {
            unitOfWork = _unitOfWork;
            bookRepository = _bookRepository;
        }

        public async Task<BookDto> ExecuteAsync(RegisterBookRequest request, CancellationToken cancellationToken)
        {
            var existingBook = await bookRepository.GetByIsbnAsync(request.Isbn, cancellationToken);

            if (existingBook != null)
            {
                throw new DuplicateIsbnException(request.Isbn);
            }

            var book = new Book(request.Title, request.Author, request.Isbn, request.TotalCopies);

            bookRepository.Add(book);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new BookDto(
                book.Id,
                book.Title,
                book.Author,
                book.ISBN,
                book.TotalCopies,
                book.AvailableCopies
            );

        }
    }
}
