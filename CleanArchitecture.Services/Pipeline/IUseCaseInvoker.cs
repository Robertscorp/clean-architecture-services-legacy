using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Pipeline
{

    public interface IUseCaseInvoker
    {

        #region - - - - - - Methods - - - - - -

        Task InvokeUseCaseAsync<TRequest, TResponse, TValidationResult>(TRequest request, IPresenter<TResponse, TValidationResult> presenter, CancellationToken cancellationToken) where TRequest : IUseCaseRequest<TResponse>;

        #endregion Methods

    }

}
