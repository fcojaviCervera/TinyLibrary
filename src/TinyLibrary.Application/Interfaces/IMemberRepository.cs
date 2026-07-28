using TinyLibrary.Domain.Models;

namespace TinyLibrary.Application.Interfaces
{
    public interface IMemberRepository
    {
        Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<Member?> GetByEmailAsync(string email, CancellationToken cancellationToken);

        void Add(Member member);
    }
}
