using CleanArchitecture.Services.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Pipeline
{

    public interface IUseCaseInvoker
    {

        #region - - - - - - Methods - - - - - -

        Task InvokeUseCaseAsync<TResponse, TValidationResult>(IUseCaseRequest<TResponse> request, IPresenter<TResponse, TValidationResult> presenter, CancellationToken cancellationToken)
            where TValidationResult : IValidationResult;

        #endregion Methods

    }

}
