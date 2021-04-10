using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Pipeline
{

    public interface IUseCaseInteractor<TRequest, TResponse, TValidationResult> where TRequest : IUseCaseRequest<TResponse>
    {

        #region - - - - - - Methods - - - - - -

        Task HandleAsync(TRequest request, IPresenter<TResponse, TValidationResult> presenter, CancellationToken cancellationToken);

        #endregion Methods

    }

}
