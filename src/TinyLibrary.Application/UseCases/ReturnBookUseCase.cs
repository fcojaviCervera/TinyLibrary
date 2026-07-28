using TinyLibrary.Application.Dtos;
using TinyLibrary.Application.Exceptions;
using TinyLibrary.Application.Interfaces;
using TinyLibrary.Domain.Services;

namespace TinyLibrary.Application.UseCases
{
    public class ReturnBookUseCase
    {
        private IUnitOfWork unitOfWork;
        private IMemberRepository memberRepository;
        private IBookRepository bookRepository;
        private ILoanRepository loanRepository;
        private TimeProvider timeProvider;
        private LoanService loanService;

        public ReturnBookUseCase(IUnitOfWork _unitOfWork, IMemberRepository _memberRepository, IBookRepository _bookRepository, ILoanRepository _loanRepository, TimeProvider _timeProvider, LoanService _loanService)
        {
            unitOfWork = _unitOfWork;
            memberRepository = _memberRepository;
            bookRepository = _bookRepository;
            loanRepository = _loanRepository;
            timeProvider = _timeProvider;
            loanService = _loanService;
        }

        public async Task<LoanDto> ExecuteAsync(Guid loanId, CancellationToken cancellationToken)
        {
            var loan = await loanRepository.GetByIdAsync(loanId, cancellationToken) ?? throw new NotFoundException($"No se encuentra el prestamo con ID {loanId}");

            var book = await bookRepository.GetByIdAsync(loan.BookId, cancellationToken) ?? throw new NotFoundException($"No se encuentra el libro con ID {loan.BookId}");

            var member = await memberRepository.GetByIdAsync(loan.MemberId, cancellationToken) ?? throw new NotFoundException($"No se encuentra el miembro con ID {loan.MemberId}");

            var now = timeProvider.GetUtcNow();

            loanService.ReturnBook(loan, book, member, now);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new LoanDto(
                loan.Id,
                loan.BookId,
                loan.MemberId,
                loan.LoanedAt,
                loan.DueAt,
                loan.ReturnedAt);
        }

    }
}
