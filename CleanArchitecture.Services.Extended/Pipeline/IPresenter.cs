using CleanArchitecture.Services.Pipeline;

namespace CleanArchitecture.Services.Extended.Pipeline
{

    public interface IPresenter<TResponse> : IPresenter<TResponse, ValidationResult>
    {
    }

}
