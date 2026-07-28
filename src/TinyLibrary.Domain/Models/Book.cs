using TinyLibrary.Domain.Exceptions;

namespace TinyLibrary.Domain.Models
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

        public Book(string title, string author, string iSBN, int totalCopies)
        {
            id = Guid.NewGuid();
            this.title = title;
            this.author = author;
            this.iSBN = iSBN;
            this.totalCopies = totalCopies;
            this.availableCopies = totalCopies;
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
