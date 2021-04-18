using CleanArchitecture.Services.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Pipeline
{

    public interface IUseCaseElement<TResponse, TValidationResult> where TValidationResult : IValidationResult
    {

        #region - - - - - - Methods - - - - - -

        Task<bool> TryPresentResultAsync<TPresenter, TRequest>(TRequest request, TPresenter presenter, CancellationToken cancellationToken)
            where TPresenter : IPresenter<TResponse, TValidationResult>
            where TRequest : IUseCaseRequest<TResponse>;

        #endregion Methods

    }

}
