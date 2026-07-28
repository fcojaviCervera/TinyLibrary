using FluentValidation;
using TinyLibrary.Application.Dtos;

namespace TinyLibrary.Application.Validators
{
    public class LendBookRequestValidator : AbstractValidator<LendBookRequest>
    {
        public LendBookRequestValidator()
        {
            RuleFor(x => x.MemberId).NotEmpty();
            RuleFor(x => x.BookId).NotEmpty();
        }
    }
}
