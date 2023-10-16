using CardStorage.Models.Requests;
using FluentValidation;

namespace CardStorage.Models.Validators;

public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequest>
{
    public AuthenticationRequestValidator()
    {
        RuleFor(e => e.Login)
            .NotNull()
            .MinimumLength(2)
            .MaximumLength(255)
            .EmailAddress();

        RuleFor(e => e.Password)
            .NotNull()
            .MinimumLength(2)
            .MaximumLength(50);
    }
}