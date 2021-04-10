using CleanArchitecture.Services.Pipeline;

namespace CleanArchitecture.Example.Application.Services
{

    public interface IUseCaseInteractor<TRequest, TResponse> : IUseCaseInteractor<IPresenter<TResponse>, TRequest, TResponse, ValidationResult>
        where TRequest : IUseCaseRequest<TResponse>
    {
    }

}
