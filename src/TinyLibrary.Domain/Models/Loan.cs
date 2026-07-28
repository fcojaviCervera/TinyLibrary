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


        public Loan(Guid _bookId, Guid _memberId, DateTimeOffset _loadnedAt, int durationDays = 14)
        {
            id = Guid.NewGuid();
            bookId = _bookId;
            memberId = _memberId;
            loanedAt = _loadnedAt;
            dueAt = loanedAt.AddDays(durationDays);
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
