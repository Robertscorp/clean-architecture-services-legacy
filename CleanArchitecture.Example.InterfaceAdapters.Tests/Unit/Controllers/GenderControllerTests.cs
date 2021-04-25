using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Example.Application.UseCases.People.GetGenders;
using CleanArchitecture.Example.InterfaceAdapters.Controllers;
using CleanArchitecture.Services.Pipeline;
using Moq;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Example.InterfaceAdapters.Tests.Unit.Controllers
{

    public class GenderControllerTests
    {

        #region - - - - - - Fields - - - - - -

        private readonly CancellationToken m_CancellationToken = new CancellationToken();
        private readonly GenderController m_Controller;

        private readonly Mock<IUseCaseInvoker> m_MockUseCaseInvoker = new Mock<IUseCaseInvoker>();

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GenderControllerTests()
            => this.m_Controller = new GenderController(this.m_MockUseCaseInvoker.Object);

        #endregion Constructors

        #region - - - - - - GetGendersAsync Tests - - - - - -

        [Fact]
        public async Task GetGendersAsync_AnyRequest_InvokesUseCaseWithPresenterAndCancellationToken()
        {
            // Arrange
            var _Presenter = new Mock<IPresenter<IQueryable<GenderDto>>>().Object;

            // Act
            await this.m_Controller.GetGendersAsync(_Presenter, this.m_CancellationToken);

            // Assert
            this.m_MockUseCaseInvoker.Verify(mock => mock.InvokeUseCaseAsync(It.IsAny<GetGendersRequest>(), _Presenter, this.m_CancellationToken));
        }

        #endregion GetGendersAsync Tests

    }

}
