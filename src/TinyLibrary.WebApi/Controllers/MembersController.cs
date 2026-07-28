using Microsoft.AspNetCore.Mvc;
using TinyLibrary.Application.Dtos;
using TinyLibrary.Application.UseCases;

namespace TinyLibrary.WebApi.Controllers
{
    [ApiController]
    [Route("socios")]
    public class MembersController : ControllerBase
    {

        private readonly RegisterMemberUseCase registerMemberUseCase;
        private readonly GetMemberLoansUseCase getMemberLoansUseCase;

        public MembersController(RegisterMemberUseCase _registerMemberUseCase, GetMemberLoansUseCase _getMemberLoansUseCase)
        {
            registerMemberUseCase = _registerMemberUseCase;
            getMemberLoansUseCase = _getMemberLoansUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterMember(RegisterMemberRequest request, CancellationToken cancellationToken)
        {
            var member = await registerMemberUseCase.ExecuteAsync(request, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, member);
        }
        [HttpGet("{id}/prestamos")]
        public async Task<ActionResult<IReadOnlyCollection<LoanDto>>> GetLoans(Guid id, CancellationToken cancellationToken)
        {
            var loans = await getMemberLoansUseCase.ExecuteAsync(id, cancellationToken);
            return Ok(loans);
        }
    }
}
