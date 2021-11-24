using FluentValidation;
using Hahn.ApplicatonProcess.July2021.Domain.Dtos;

namespace Hahn.ApplicatonProcess.July2021.Domain.Validators
{
    public class AddUpdateUserValidator : AbstractValidator<CreateUserDto>
    {
        public AddUpdateUserValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("FirstName is required.");
            RuleFor(x => x.FirstName).MinimumLength(3);
            RuleFor(x => x.FirstName).MaximumLength(20);
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("LastName is required.");
            RuleFor(x => x.LastName).MinimumLength(3);
            RuleFor(x => x.LastName).MaximumLength(20);
            RuleFor(x => x.Age).GreaterThan(18);
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email address is required").EmailAddress().WithMessage("A valid email is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required").Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{8,}$");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("ConfirmPassword is required")
                .When(x => x.Password.Equals(x.ConfirmPassword)).WithMessage("Password and ConfirmPassword must be the same");
            RuleFor(x => x.Address).NotNull().NotEmpty().WithMessage("Address is required.");
        }
    }
}
