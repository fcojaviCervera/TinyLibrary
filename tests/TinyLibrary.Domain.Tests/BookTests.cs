using TinyLibrary.Domain.Exceptions;

namespace TinyLibrary.Domain.Tests
{
    public class BookTests
    {
        [Test]
        public void Lend_WhenCopiesAvailable_DecreasesAvailableCopies()
        {
            var book = new Book("Test Title", "Test Author", "1234567890", 5);

            book.LendCopy();

            Assert.That(book.AvailableCopies, Is.EqualTo(4));

        }

        [Test]
        public void Lend_WhenNoCopiesAvailable_ThrowsNoAvailableCopiesException()
        {
            Assert.Throws<NoAvailableCopiesException>(() =>
            {
                var book = new Book("Test Title", "Test Author", "1234567890", 0);

                book.LendCopy();

            });

        }

        [Test]
        public void Return_IncreasesAvailableCopies()
        {
            var book = new Book("Test Title", "Test Author", "1234567890", 5);

            book.LendCopy();

            book.ReturnCopy();

            Assert.That(book.AvailableCopies, Is.EqualTo(5));
        }

        [Test]
        public void Return_WhenAllCopiesAlreadyReturned_ThrowsException()
        {
            Assert.Throws<AllCopiesAlreadyAvailableException>(() =>
            {
                var book = new Book("Test Title", "Test Author", "1234567890", 5);
                book.ReturnCopy();
            });
        }

    }
}
