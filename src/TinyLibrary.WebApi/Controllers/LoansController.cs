using Microsoft.AspNetCore.Mvc;
using TinyLibrary.Application.Dtos;
using TinyLibrary.Application.UseCases;

namespace TinyLibrary.WebApi.Controllers
{
    [ApiController]
    [Route("prestamos")]
    public class LoansController : ControllerBase
    {
        private readonly LendBookUseCase lendBookUseCase;
        private readonly ReturnBookUseCase returnBookUseCase;

        public LoansController(LendBookUseCase _lendBookUseCase, ReturnBookUseCase _returnBookUseCase)
        {
            lendBookUseCase = _lendBookUseCase;
            returnBookUseCase = _returnBookUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> LendBook(LendBookRequest request, CancellationToken cancellationToken)
        {
            var loan = await lendBookUseCase.ExecuteAsync(request, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, loan);
        }

        [HttpPost("{id}/devolver")]
        public async Task<IActionResult> ReturnBook(Guid id, CancellationToken cancellationToken)
        {
            var loan = await returnBookUseCase.ExecuteAsync(id, cancellationToken);
            return Ok(loan);
        }

    }
}
