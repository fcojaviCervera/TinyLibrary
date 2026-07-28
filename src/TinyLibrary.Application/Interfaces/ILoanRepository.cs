using TinyLibrary.Domain.Models;

namespace TinyLibrary.Application.Interfaces
{
    public interface ILoanRepository
    {
        Task<Loan?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Loan>> GetActiveByMemberIdAsync(Guid memberId,CancellationToken cancellationToken);
        Task<IReadOnlyCollection<Loan>> GetByMemberIdAsync(Guid memberId, CancellationToken cancellationToken);
        void Add(Loan loan);
    }
}
