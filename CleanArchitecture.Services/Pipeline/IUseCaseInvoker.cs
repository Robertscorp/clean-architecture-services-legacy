using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Pipeline
{

    public interface IUseCaseInvoker
    {

        #region - - - - - - Methods - - - - - -

        Task InvokeUseCaseAsync<TRequest, TResponse, TValidationFailure>(TRequest request, IPresenter<TResponse, TValidationFailure> presenter, CancellationToken cancellationToken) where TRequest : IUseCaseRequest<TResponse>;

        #endregion Methods

    }

}
