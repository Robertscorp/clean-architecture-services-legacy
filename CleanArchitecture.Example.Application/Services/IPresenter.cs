using CleanArchitecture.Services.Pipeline;

namespace CleanArchitecture.Example.Application.Services
{

    public interface IPresenter<TResponse> : IPresenter<TResponse, ValidationResult>
    {
    }

}
