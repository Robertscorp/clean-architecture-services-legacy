using CleanArchitecture.Services.Extended.FluentValidation;
using CleanArchitecture.Services.Pipeline;

namespace CleanArchitecture.Services.Extended.Pipeline
{

    public interface IUseCaseInteractor<TRequest, TResponse> : IUseCaseInteractor<IPresenter<TResponse>, TRequest, TResponse, ValidationResult>
        where TRequest : IUseCaseRequest<TResponse>
    {
    }

}
