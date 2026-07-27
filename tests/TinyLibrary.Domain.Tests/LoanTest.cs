using NUnit.Framework.Internal;
using TinyLibrary.Domain.Exceptions;

namespace TinyLibrary.Domain.Tests
{
    public class LoanTest
    {
        [Test]
        public void Constructor_SetsDueAt14DaysAfterLoanedAt()
        {
            var bookId = Guid.NewGuid();
            var memberId = Guid.NewGuid();
            var loanedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

            var loan = new Loan(bookId, memberId,loanedAt);

            Assert.That(loan.DueAt, Is.EqualTo(loanedAt.AddDays(14)));
        }

        [Test]
        public void IsOverdue_WhenNowIsAfterDueAtAndNotReturned_ReturnsTrue()
        {
            var bookId = Guid.NewGuid();
            var memberId = Guid.NewGuid();
            var loanedAt = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);

            var loan = new Loan(bookId, memberId, loanedAt);

            Assert.That(loan.IsOverdue(DateTimeOffset.Now), Is.True);
        }

        [Test]
        public void IsOverdue_WhenNowIsBeforeDueAt_ReturnsFalse()
        {
            var bookId = Guid.NewGuid();
            var memberId = Guid.NewGuid();
            var loanedAt = DateTimeOffset.Now.AddDays(-5);

            var loan = new Loan(bookId, memberId, loanedAt);

            Assert.That(loan.IsOverdue(DateTimeOffset.Now), Is.False);
        }

        [Test]
        public void IsOverdue_WhenAlreadyReturned_ReturnsFalse()
        {
            var bookId = Guid.NewGuid();
            var memberId = Guid.NewGuid();
            var loanedAt = DateTimeOffset.Now.AddDays(-20);

            var loan = new Loan(bookId, memberId, loanedAt);

            loan.ReturnBook(DateTimeOffset.Now);

            Assert.That(loan.IsOverdue(DateTimeOffset.Now), Is.False);

        }

        [Test]
        public void Return_WhenMoreThan7DaysLate_ReturnsTrue()
        {
            var bookId = Guid.NewGuid();
            var memberId = Guid.NewGuid();
            var loanedAt = DateTimeOffset.Now.AddDays(-25);

            var loan = new Loan(bookId, memberId, loanedAt);

            Assert.That(loan.ReturnBook(DateTimeOffset.Now),Is.True);
        }

        [Test]
        public void Return_WhenLessThan7DaysLate_ReturnsFalse()
        {
            var bookId = Guid.NewGuid();
            var memberId = Guid.NewGuid();
            var loanedAt = DateTimeOffset.Now.AddDays(-16);

            var loan = new Loan(bookId, memberId, loanedAt);

            Assert.That(loan.ReturnBook(DateTimeOffset.Now), Is.False);


        }
        [Test]
        public void Return_WhenAlreadyReturned_ThrowsLoanAlreadyReturnedException()
        {
            var bookId = Guid.NewGuid();
            var memberId = Guid.NewGuid();
            var loanedAt = DateTimeOffset.Now.AddDays(-5);

            var loan = new Loan(bookId, memberId, loanedAt);

            loan.ReturnBook(DateTimeOffset.Now);

            Assert.Throws<LoanAlreadyReturnedException>(() => loan.ReturnBook(DateTimeOffset.Now));

        }

    }
}
