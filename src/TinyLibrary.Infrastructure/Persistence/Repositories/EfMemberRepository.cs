using Microsoft.EntityFrameworkCore;
using TinyLibrary.Application.Interfaces;
using TinyLibrary.Domain.Models;

namespace TinyLibrary.Infrastructure.Persistence.Repositories
{
    public class EfMemberRepository : IMemberRepository
    {
        private readonly LibraryDbContext context;

        public EfMemberRepository(LibraryDbContext _context)
        {
            context = _context;
        }

        public void Add(Member member)
        {
            context.Members.Add(member);
        }

        public async Task<Member?> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await context.Members.FirstOrDefaultAsync(m => m.Email == email, cancellationToken);
        }

        public async Task<Member?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await context.Members.FirstOrDefaultAsync(m => m.Id == id, cancellationToken);
        }
    }
}
