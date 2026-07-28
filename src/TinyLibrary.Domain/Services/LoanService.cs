using TinyLibrary.Domain.Exceptions;
using TinyLibrary.Domain.Models;

namespace TinyLibrary.Domain.Services
{
    public class LoanService
    {

        public Loan LendBook(Book book, Member member, IReadOnlyCollection<Loan> memberActiveLoans, DateTimeOffset now)
        {
            if (member.IsPenalized(now))
                throw new MemberPenalizedException();
            if (memberActiveLoans.Count >= 3)
                throw new MaxActiveLoansExceededException();
            if (memberActiveLoans.Any(l => l.IsOverdue(now)))
                throw new OverdueLoanPendingException();

            book.LendCopy();
            return new Loan(book.Id, member.Id, now);
        }

        public void ReturnBook(Loan loan, Book book, Member member, DateTimeOffset now)
        {
            var wasLate = loan.ReturnBook(now);

            book.ReturnCopy();

            if (wasLate)
                member.Penalize(now.AddDays(30));
            
        }


    }
}
