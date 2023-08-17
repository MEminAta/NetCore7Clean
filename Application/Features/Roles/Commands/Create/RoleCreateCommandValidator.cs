using FluentValidation;

namespace Application.Features.Roles.Commands.Create;

public class RoleCreateCommandValidator: AbstractValidator<RoleCreateCommand>
{
    public RoleCreateCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2);
    }
}