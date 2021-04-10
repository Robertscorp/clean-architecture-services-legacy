using CleanArchitecture.Services.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Pipeline
{

    public interface IPresenter<TResponse, TValidationFailure>
    {

        #region - - - - - - Methods - - - - - -

        Task PresentAsync(TResponse response, CancellationToken cancellationToken);

        Task PresentEntityNotFoundAsync(EntityRequest entityRequest, CancellationToken cancellationToken);

        Task PresentValidationFailureAsync(TValidationFailure validationFailure, CancellationToken cancellationToken);

        #endregion Methods

    }

}
