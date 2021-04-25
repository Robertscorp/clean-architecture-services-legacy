using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Example.Application.UseCases.People.GetGenders;
using CleanArchitecture.Services.Pipeline;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Example.InterfaceAdapters.Controllers
{

    public class GenderController
    {

        #region - - - - - - Fields - - - - - -

        private readonly IUseCaseInvoker m_UseCaseInvoker;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GenderController(IUseCaseInvoker useCaseInvoker)
            => this.m_UseCaseInvoker = useCaseInvoker ?? throw new ArgumentNullException(nameof(useCaseInvoker));

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public Task GetGendersAsync(IPresenter<IQueryable<GenderDto>> presenter, CancellationToken cancellationToken)
            => this.m_UseCaseInvoker.InvokeUseCaseAsync(new GetGendersRequest(), presenter, cancellationToken);

        #endregion Methods

    }

}
