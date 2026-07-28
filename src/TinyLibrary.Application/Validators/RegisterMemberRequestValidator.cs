using FluentValidation;
using TinyLibrary.Application.Dtos;

namespace TinyLibrary.Application.Validators
{
    public class RegisterMemberRequestValidator : AbstractValidator<RegisterMemberRequest>
    {
        public RegisterMemberRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}
