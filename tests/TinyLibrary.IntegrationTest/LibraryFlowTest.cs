using System.Net;
using System.Net.Http.Json;
using TinyLibrary.Application.Dtos;

namespace TinyLibrary.IntegrationTest
{
    public class LibraryFlowTest
    {
        private TinyLibraryWebApplicationFactory factory = null!;
        private HttpClient client = null!;

        [SetUp]
        public void Setup()
        {
            factory = new TinyLibraryWebApplicationFactory();
            client = factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            client.Dispose();
            factory.Dispose();
        }


        [Test]
        public async Task FullFlow_LateReturnPenalizesMember_ThenNewLoanIsRejected()
        {
            // Register a new member
            var registerMemberRequest = new
            {
                Name = "John Doe",
                Email = "test@test.com"
            };

            var registerMemberResponse = await client.PostAsJsonAsync("/socios", registerMemberRequest);

            registerMemberResponse.EnsureSuccessStatusCode();
            Assert.That(registerMemberResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            //Register a new book
            var registerBookRequest = new
            {
                Title = "Test Book",
                Author = "Test Author",
                Isbn = "1234567890",
                TotalCopies = 3
            };

            var registerBookResponse = await client.PostAsJsonAsync("/libros", registerBookRequest);

            registerBookResponse.EnsureSuccessStatusCode();
            Assert.That(registerBookResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));


            var registeredMember = await registerMemberResponse.Content.ReadFromJsonAsync<MemberDto>();
            var registeredBook = await registerBookResponse.Content.ReadFromJsonAsync<BookDto>();

            // Lend the book to the member

            var lendBookRequest = new 
            {
                MemberId = registeredMember.Id,
                BookId = registeredBook.Id
            };

            var lendBookResponse = await client.PostAsJsonAsync("/prestamos", lendBookRequest);


            lendBookResponse.EnsureSuccessStatusCode();
            Assert.That(lendBookResponse.StatusCode, Is.EqualTo(HttpStatusCode.Created));


            var lendBook = await lendBookResponse.Content.ReadFromJsonAsync<LoanDto>();

            //Advance time
            factory.TimeProvider.Advance(TimeSpan.FromDays(25));

            //Return book offtime
            var returnBookResponse = await client.PostAsync($"/prestamos/{lendBook.Id}/devolver", null);
            returnBookResponse.EnsureSuccessStatusCode();
            Assert.That(returnBookResponse.StatusCode, Is.EqualTo(HttpStatusCode.OK));


            //Try to lend again
            var lendBookAgainResponse = await client.PostAsJsonAsync("/prestamos", lendBookRequest);

            Assert.That(lendBookAgainResponse.StatusCode, Is.EqualTo(HttpStatusCode.Conflict));

        }
    }
}
