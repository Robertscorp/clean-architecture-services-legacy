using CleanArchitecture.Services.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Pipeline
{

    public interface IUseCaseInteractor<TPresenter, TRequest, TResponse, TValidationResult>
        where TPresenter : IPresenter<TResponse, TValidationResult>
        where TRequest : IUseCaseRequest<TResponse>
        where TValidationResult : IValidationResult
    {

        #region - - - - - - Methods - - - - - -

        Task HandleAsync(TRequest request, TPresenter presenter, CancellationToken cancellationToken);

        #endregion Methods

    }

}
