using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TinyLibrary.Application.Interfaces;
using TinyLibrary.Domain.Models;

namespace TinyLibrary.Infrastructure.Persistence.Repositories
{
    public class EfLoanRepository : ILoanRepository
    {
        private readonly LibraryDbContext context;

        public EfLoanRepository(LibraryDbContext _context)
        {
            context = _context;
        }

        public void Add(Loan loan)
        {
            context.Loans.Add(loan);
        }

        public async Task<Loan?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await context.Loans.FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
        }        

        public async Task<IReadOnlyCollection<Loan>> GetByMemberIdAsync(Guid memberId, CancellationToken cancellationToken)
        {
            return await context.Loans.Where(l => l.MemberId == memberId).ToListAsync(cancellationToken);
        }
        public async Task<IReadOnlyCollection<Loan>> GetActiveByMemberIdAsync(Guid memberId, CancellationToken cancellationToken)
        {
            return await context.Loans.Where(l => l.MemberId == memberId && l.ReturnedAt == null).ToListAsync(cancellationToken);
        }

    }
}
