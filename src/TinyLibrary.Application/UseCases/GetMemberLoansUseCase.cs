using TinyLibrary.Application.Dtos;
using TinyLibrary.Application.Exceptions;
using TinyLibrary.Application.Interfaces;

namespace TinyLibrary.Application.UseCases
{
    public class GetMemberLoansUseCase
    {
        public IMemberRepository memberRepository;
        public ILoanRepository loanRepository;

        public GetMemberLoansUseCase(IMemberRepository _memberRepository, ILoanRepository _loanRepository)
        {
            memberRepository = _memberRepository;
            loanRepository = _loanRepository;
        }

        public async Task<IReadOnlyCollection<LoanDto>> ExecuteAsync(Guid memberId, CancellationToken cancellationToken)
        {
            var member = await memberRepository.GetByIdAsync(memberId, cancellationToken) ?? throw new NotFoundException($"No se encontró el socio con ID {memberId}");
            var loans = await loanRepository.GetByMemberIdAsync(memberId, cancellationToken);
            return loans.Select(l => new LoanDto
            (
                l.Id,
                l.BookId,
                l.MemberId,
                l.LoanedAt,
                l.DueAt,
                l.ReturnedAt
            )).ToList());
        }
}
