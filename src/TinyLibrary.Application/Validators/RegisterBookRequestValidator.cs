using FluentValidation;
using TinyLibrary.Application.Dtos;

namespace TinyLibrary.Application.Validators
{
    public class RegisterBookRequestValidator : AbstractValidator<RegisterBookRequest>
    {
        public RegisterBookRequestValidator() 
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Author).NotEmpty();
            RuleFor(x => x.Isbn).NotEmpty();
            RuleFor(x => x.TotalCopies).GreaterThan(0);
        }
    }
}
