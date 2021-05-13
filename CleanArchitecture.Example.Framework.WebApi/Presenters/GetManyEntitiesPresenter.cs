using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Extended.FluentValidation;
using CleanArchitecture.Services.Extended.Pipeline;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Example.Framework.WebApi.Presenters
{

    public class GetManyEntitiesPresenter<TResponse> : IPresenter<IQueryable<TResponse>>
    {

        #region - - - - - - Properties - - - - - -

        public IActionResult ActionResult { get; private set; }

        #endregion Properties

        #region - - - - - - IPresenter Implementation - - - - - -

        public Task PresentAsync(IQueryable<TResponse> response, CancellationToken cancellationToken)
        {
            this.ActionResult = new OkObjectResult(response);
            return Task.CompletedTask;
        }

        public Task PresentEntityNotFoundAsync(EntityID entityID, CancellationToken cancellationToken)
            => throw new NotImplementedException();

        public Task PresentValidationFailureAsync(ValidationResult validationResult, CancellationToken cancellationToken)
            => throw new NotImplementedException();

        #endregion IPresenter Implementation

    }

}
