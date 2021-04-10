using CleanArchitecture.Services.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Pipeline
{

    public interface IUseCaseInvoker
    {

        #region - - - - - - Methods - - - - - -

        Task InvokeUseCaseAsync<TPresenter, TRequest, TResponse, TValidationResult>(TRequest request, TPresenter presenter, CancellationToken cancellationToken)
            where TPresenter : IPresenter<TResponse, TValidationResult>
            where TRequest : IUseCaseRequest<TResponse>
            where TValidationResult : IValidationResult;

        #endregion Methods

    }

}
