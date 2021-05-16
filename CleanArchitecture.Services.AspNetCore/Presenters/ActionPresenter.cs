using AutoMapper;
using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Extended.FluentValidation;
using CleanArchitecture.Services.Extended.Pipeline;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.AspNetCore.Presenters
{

    public abstract class ActionPresenter<TResponse> : IPresenter<TResponse>
    {

        #region - - - - - - Properties - - - - - -

        public IActionResult ActionResult { get; protected set; }

        public IMapper Mapper { get; set; }

        public bool PresentedSuccessfully { get; protected set; }

        #endregion Properties

        #region - - - - - - IPresenter Implementation - - - - - -

        public abstract Task PresentAsync(TResponse response, CancellationToken cancellationToken);

        public virtual Task PresentEntityNotFoundAsync(EntityID entityID, CancellationToken cancellationToken)
        {
            this.ActionResult = new NotFoundResult();
            return Task.CompletedTask;
        }

        public virtual Task PresentValidationFailureAsync(ValidationResult validationResult, CancellationToken cancellationToken)
        {
            this.ActionResult = new BadRequestObjectResult(this.Mapper.Map<ValidationProblemDetails>(validationResult));
            return Task.CompletedTask;
        }

        #endregion IPresenter Implementation

    }

}
