using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Pipeline;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Internal
{

    public abstract class InternalUseCaseInvoker
    {

        #region - - - - - - Methods - - - - - -

        public abstract Task InvokeUseCaseAsync(CancellationToken cancellationToken);

        #endregion Methods

    }

    public class InternalUseCaseInvoker<TPresenter, TRequest, TResponse, TValidationResult> : InternalUseCaseInvoker
        where TPresenter : IPresenter<TResponse, TValidationResult>
        where TRequest : IUseCaseRequest<TResponse>
        where TValidationResult : IValidationResult
    {

        #region - - - - - - Fields - - - - - -

        private readonly TPresenter m_Presenter;
        private readonly TRequest m_Request;
        private readonly IServiceProvider m_ServiceProvider;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public InternalUseCaseInvoker(TPresenter presenter, TRequest request, IServiceProvider serviceProvider)
        {
            this.m_Presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
            this.m_Request = request ?? throw new ArgumentNullException(nameof(request));
            this.m_ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public override async Task InvokeUseCaseAsync(CancellationToken cancellationToken)
        {
            var _UseCaseElements = (IEnumerable<IUseCaseElement<TResponse, TValidationResult>>)this.m_ServiceProvider.GetService(typeof(IEnumerable<IUseCaseElement<TResponse, TValidationResult>>));

            foreach (var _UseCaseElement in _UseCaseElements)
                if (await _UseCaseElement.TryPresentResultAsync(this.m_Request, this.m_Presenter, cancellationToken))
                    return;
        }

        #endregion Methods

    }

}
