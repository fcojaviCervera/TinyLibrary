using TinyLibrary.Domain.Exceptions;
using TinyLibrary.Domain.Models;
using TinyLibrary.Domain.Services;

namespace TinyLibrary.Domain.Tests
{
    public class LoanServiceTests
    {

        [Test]
        public void LendBook_WhenMemberHasThreeActiveLoans_ThrowsMaxActiveLoansExceededException()
        {
            var now = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var book = new Book("Test Book", "Test Author", "1234567890", 1);
            var member = new Member("Test Name", "test@test.com", now.AddYears(-1));

            var activeLoans = new List<Loan>
            {
                new(Guid.NewGuid(), member.Id,  now),
                new(Guid.NewGuid(), member.Id, now),
                new(Guid.NewGuid(), member.Id, now)
            };

            var loanService = new LoanService();

            Assert.Throws<MaxActiveLoansExceededException>(() => loanService.LendBook(book, member, activeLoans, now));

        }

        [Test]
        public void LendBook_WhenNoCopiesAvailable_ThrowsNoAvailableCopiesException()
        {
            var now = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var book = new Book("Test Book", "Test Author", "1234567890", 0);
            var member = new Member("Test Name", "test@test.com", now.AddYears(-1));

            var activeLoans = new List<Loan>
            {
                new(Guid.NewGuid(), member.Id,  now)
            };

            var loanService = new LoanService();

            Assert.Throws<NoAvailableCopiesException>(() => loanService.LendBook(book, member, activeLoans, now));
        }

        [Test]
        public void LendBook_WhenMemberIsPenalized_ThrowsMemberPenalizedException()
        {
            var now = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var book = new Book("Test Book", "Test Author", "1234567890", 0);
            var member = new Member("Test Name", "test@test.com", now.AddYears(-1));

            member.Penalize(now.AddDays(1));

            var activeLoans = new List<Loan>();

            var loanService = new LoanService();

            Assert.Throws<MemberPenalizedException>(() => loanService.LendBook(book, member, activeLoans, now));
        }

        [Test]
        public void LendBook_WhenMemberHasOverdueLoan_ThrowsOverdueLoanPendingException()
        {
            var now = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var book = new Book("Test Book", "Test Author", "1234567890", 0);
            var member = new Member("Test Name", "test@test.com", now.AddYears(-1));

            var activeLoans = new List<Loan>
            {
                new(Guid.NewGuid(), member.Id,  now.AddDays(-30))
            };

            var loanService = new LoanService();

            Assert.Throws<OverdueLoanPendingException>(() => loanService.LendBook(book, member, activeLoans, now));
        }

        [Test]
        public void LendBook_CreatesLoanDueIn14DaysFromNow()
        {
            var now = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var book = new Book("Test Book", "Test Author", "1234567890", 1);
            var member = new Member("Test Name", "test@test.com", now.AddYears(-1));

            var loanService = new LoanService();

            var loan = loanService.LendBook(book, member, [], now);

            Assert.That(loan.DueAt, Is.EqualTo(now.AddDays(14)));

        }

        [Test]
        public void ReturnBook_WhenMoreThan7DaysLate_PenalizesMemberFor30Days()
        {
            var now = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var book = new Book("Test Book", "Test Author", "1234567890", 1);
            var member = new Member("Test Name", "test@test.com", now.AddYears(-1));


            var loanService = new LoanService();

            var loan = loanService.LendBook(book, member, [], now.AddDays(-30));
            loanService.ReturnBook(loan, book, member, now);

            Assert.That(member.IsPenalized(now), Is.True);
            Assert.That(member.PenalizedUntil, Is.EqualTo(now.AddDays(30)));

        }

        [Test]
        public void ReturnBook_WhenNotLate_DoesNotPenalizeMember()
        {
            var now = new DateTimeOffset(2026, 1, 1, 0, 0, 0, TimeSpan.Zero);
            var book = new Book("Test Book", "Test Author", "1234567890", 1);
            var member = new Member("Test Name", "test@test.com", now.AddYears(-1));


            var loanService = new LoanService();

            var loan = loanService.LendBook(book, member, [], now.AddDays(-5));
            loanService.ReturnBook(loan, book, member, now);
            Assert.That(member.IsPenalized(now), Is.False);
            Assert.That(member.PenalizedUntil, Is.Null);

        }
    }
}
