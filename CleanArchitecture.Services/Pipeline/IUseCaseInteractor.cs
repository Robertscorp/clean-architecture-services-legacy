using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Pipeline
{

    public interface IUseCaseInteractor<TRequest, TResponse, TValidationFailure> where TRequest : IUseCaseRequest<TResponse>
    {

        #region - - - - - - Methods - - - - - -

        Task HandleAsync(TRequest request, IPresenter<TResponse, TValidationFailure> presenter, CancellationToken cancellationToken);

        #endregion Methods

    }

}
