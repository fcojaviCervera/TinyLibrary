using TinyLibrary.Application.Dtos;
using TinyLibrary.Application.Exceptions;
using TinyLibrary.Application.Interfaces;
using TinyLibrary.Domain.Services;

namespace TinyLibrary.Application.UseCases
{
    public class LendBookUseCase
    {
        private IUnitOfWork unitOfWork;
        private IMemberRepository memberRepository;
        private IBookRepository bookRepository;
        private ILoanRepository loanRepository;
        private TimeProvider timeProvider;
        private LoanService loanService;

        public LendBookUseCase(IUnitOfWork _unitOfWork, IMemberRepository _memberRepository, IBookRepository _bookRepository, ILoanRepository _loanRepository, TimeProvider _timeProvider, LoanService _loanService)
        {
            unitOfWork = _unitOfWork;
            memberRepository = _memberRepository;
            bookRepository = _bookRepository;
            loanRepository = _loanRepository;
            timeProvider = _timeProvider;
            loanService = _loanService;
        }

        public async Task<LoanDto> ExecuteAsync(LendBookRequest request, CancellationToken cancellationToken)
        {
            var book = await bookRepository.GetByIdAsync(request.BookId, cancellationToken);
            if (book == null)
            {
                throw new NotFoundException($"No se encontró el libro con ID {request.BookId}");
            }

            var member = await memberRepository.GetByIdAsync(request.MemberId, cancellationToken);
            if (member == null)
            {
                throw new NotFoundException($"No se encontró el socio con ID {request.MemberId}");
            }

            var activeLoans = await loanRepository.GetActiveByMemberIdAsync(member.Id, cancellationToken);

            var now = timeProvider.GetUtcNow();

            var loan = loanService.LendBook(book, member, activeLoans, now);

            loanRepository.Add(loan);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return new LoanDto
            (
                loan.Id,
                loan.BookId,
                loan.MemberId,
                loan.LoanedAt,
                loan.DueAt,
                loan.ReturnedAt
            );
        }
    }
}
