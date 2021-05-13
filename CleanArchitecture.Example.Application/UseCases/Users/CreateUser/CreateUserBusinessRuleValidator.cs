using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Extended.Pipeline;
using CleanArchitecture.Services.Persistence;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Example.Application.UseCases.Users.CreateUser
{

    public class CreateUserBusinessRuleValidator : IBusinessRuleValidator<CreateUserRequest>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreateUserBusinessRuleValidator(IPersistenceContext persistenceContext)
            => this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));

        #endregion Constructors

        #region - - - - - - IBusinessRuleValidator Implementation - - - - - -

        public async Task<ValidationResult> ValidateAsync(CreateUserRequest request, CancellationToken cancellationToken)
            => (await this.m_PersistenceContext.GetEntitiesAsync<User>(cancellationToken)).Any(u => u.UserName == request.UserName)
                ? ValidationResult.Failure("A user with that user name already exists.")
                : ValidationResult.Success();

        #endregion IBusinessRuleValidator Implementation

    }

}
