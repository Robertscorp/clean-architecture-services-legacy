using CleanArchitecture.Services.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Pipeline
{

    public interface IPresenter<TResponse, TValidationResult>
    {

        #region - - - - - - Methods - - - - - -

        Task PresentAsync(TResponse response, CancellationToken cancellationToken);

        Task PresentEntityNotFoundAsync(EntityRequest entityRequest, CancellationToken cancellationToken);

        Task PresentValidationFailureAsync(TValidationResult validationResult, CancellationToken cancellationToken);

        #endregion Methods

    }

}
