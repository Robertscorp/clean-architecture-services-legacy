using CleanArchitecture.Services.Pipeline;

namespace CleanArchitecture.Example.Application.Services.Pipeline
{

    public interface IPresenter<TResponse> : IPresenter<TResponse, ValidationResult>
    {
    }

}
