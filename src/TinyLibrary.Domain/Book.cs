using TinyLibrary.Domain.Exceptions;

namespace TinyLibrary.Domain
{
    public class Book
    {
        private readonly Guid id;
        private readonly string title;
        private readonly string author;
        private readonly string iSBN;
        private int totalCopies;
        private int availableCopies;

        public Guid Id => id;

        public string Title => title;

        public string Author => author;

        public string ISBN => iSBN;

        public int TotalCopies  => totalCopies; 
        public int AvailableCopies => availableCopies; 

        public Book(string _title, string _author, string _iSBN, int _totalCopies)
        {
            id = Guid.NewGuid();
            title = _title;
            author = _author;
            iSBN = _iSBN;
            totalCopies = _totalCopies;
            availableCopies = _totalCopies;
        }

        public void LendCopy()
        {
            if (totalCopies <= 0 || availableCopies <= 0)
            {
                throw new NoAvailableCopiesException();
            }
            availableCopies--;
        }

        public void ReturnCopy()
        {
            if (availableCopies >= totalCopies)
            {
                throw new AllCopiesAlreadyAvailableException();
            }
            availableCopies++;
        }
    }
}
