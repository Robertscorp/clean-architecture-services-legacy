using CleanArchitecture.Services.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Pipeline
{

    public interface IUseCaseInvoker
    {

        #region - - - - - - Methods - - - - - -

        Task InvokeUseCaseAsync<TRequest, TResponse, TValidationResult>(TRequest request, IPresenter<TResponse, TValidationResult> presenter, CancellationToken cancellationToken)
            where TRequest : IUseCaseRequest<TResponse>
            where TValidationResult : IValidationResult;

        #endregion Methods

    }

}
