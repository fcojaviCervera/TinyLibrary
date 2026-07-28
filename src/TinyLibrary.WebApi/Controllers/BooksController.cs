using Microsoft.AspNetCore.Mvc;
using TinyLibrary.Application.Dtos;
using TinyLibrary.Application.UseCases;

namespace TinyLibrary.WebApi.Controllers
{
    [ApiController]
    [Route("libros")]
    public class BooksController : ControllerBase
    {

        private readonly RegisterBookUseCase registerBookUseCase;
        private readonly GetCatalogUseCase getCatalogUseCase;

        public BooksController(RegisterBookUseCase _registerBookUseCase, GetCatalogUseCase _getCatalogUseCase)
        {
            registerBookUseCase = _registerBookUseCase;
            getCatalogUseCase = _getCatalogUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterBook([FromBody] RegisterBookRequest request, CancellationToken cancellationToken)
        {
            var book = await registerBookUseCase.ExecuteAsync(request, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, book);
        }

        [HttpGet]
        public async Task<IActionResult> GetCatalog(CancellationToken cancellationToken)
        {
            var books = await getCatalogUseCase.ExecuteAsync(cancellationToken);
            return Ok(books);
        }
    }
}
