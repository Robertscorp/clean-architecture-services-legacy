using CleanArchitecture.Services.Extended.FluentValidation;
using FluentValidation;

namespace CleanArchitecture.Example.Application.UseCases.Users.CreateUser
{

    public class CreateUserRequestValidator : Validator<CreateUserRequest>
    {

        #region - - - - - - Constructors - - - - - -

        public CreateUserRequestValidator()
        {
            _ = this.RuleFor(r => r.EmployeeRoleID).NotNull();
            _ = this.RuleFor(r => r.FirstName).NotEmpty().MaximumLength(50);
            _ = this.RuleFor(r => r.GenderID).NotNull();
            _ = this.RuleFor(r => r.LastName).NotEmpty().MaximumLength(50);
            _ = this.RuleFor(r => r.PlainTextPassword).NotEmpty().MaximumLength(100);
            _ = this.RuleFor(r => r.UserName).NotEmpty().MaximumLength(100);
        }

        #endregion Constructors

    }

}
