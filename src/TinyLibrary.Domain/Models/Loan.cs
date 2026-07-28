using TinyLibrary.Domain.Exceptions;

namespace TinyLibrary.Domain.Models
{
    public class Loan
    {
        private readonly Guid id;
        private readonly Guid bookId;
        private readonly Guid memberId;
        private readonly DateTimeOffset loanedAt;
        private readonly DateTimeOffset dueAt;
        private DateTimeOffset? returnedAt;
        public Guid Id => id;
        public Guid BookId => bookId; 
        public Guid MemberId => memberId; 
        public DateTimeOffset LoanedAt => loanedAt; 
        public DateTimeOffset DueAt => dueAt; 
        public DateTimeOffset? ReturnedAt => returnedAt;


        public Loan(Guid bookId, Guid memberId, DateTimeOffset loanedAt)
        {
            id = Guid.NewGuid();
            this.bookId = bookId;
            this.memberId = memberId;
            this.loanedAt = loanedAt;
            dueAt = loanedAt.AddDays(14);
            returnedAt = null;
        }

        public bool IsOverdue(DateTimeOffset now)
        {
            return returnedAt == null && now > dueAt;
        }

        public bool ReturnBook(DateTimeOffset _returnedAt)
        {
            if (ReturnedAt != null)
            {
                throw new LoanAlreadyReturnedException();
            }
            returnedAt = _returnedAt;

            //Devuelve true si el libro se devuelve después de 7 días de la fecha de vencimiento
            return _returnedAt > DueAt.AddDays(7);
        }

    }
}
