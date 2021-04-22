using AutoMapper;
using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Example.Application.UseCases.Customers.CreateCustomer;
using CleanArchitecture.Example.Application.UseCases.People.GetGenders;
using CleanArchitecture.Example.InterfaceAdapters.ViewModels.Customers;
using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Pipeline;
using FluentAssertions;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Example.InterfaceAdapters.Tests.Unit.ViewModels.Customers
{

    public class NewCustomerViewModelTests
    {

        #region - - - - - - Fields - - - - - -

        private readonly CancellationToken m_CancellationToken = new CancellationToken();
        private readonly NewCustomerViewModel m_ViewModel;

        private readonly Mock<IMapper> m_MockMapper = new Mock<IMapper>();
        private readonly Mock<IUseCaseInvoker> m_MockUseCaseInvoker = new Mock<IUseCaseInvoker>();

        private readonly Mock<Action<ExistingCustomerViewModel>> m_MockOnCustomerCreatedAction = new Mock<Action<ExistingCustomerViewModel>>();
        private readonly Mock<Action<ValidationResult>> m_MockOnValidationFailureAction = new Mock<Action<ValidationResult>>();

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public NewCustomerViewModelTests()
            => this.m_ViewModel = new NewCustomerViewModel(this.m_MockMapper.Object, this.m_MockUseCaseInvoker.Object)
            {
                OnCustomerCreated = this.m_MockOnCustomerCreatedAction.Object,
                OnValidationFailure = this.m_MockOnValidationFailureAction.Object
            };

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void VerifyNoOtherCalls()
        {
            // Ignored: MockMapper
            this.m_MockUseCaseInvoker.VerifyNoOtherCalls();
            this.m_MockOnCustomerCreatedAction.VerifyNoOtherCalls();
            this.m_MockOnValidationFailureAction.VerifyNoOtherCalls();
        }

        #endregion Methods

        #region - - - - - - CreateCustomerAsync Tests - - - - - -

        [Fact]
        public async Task CreateCustomerAsync_Any_InvokesCreateCustomerUseCase()
        {
            // Arrange

            // Act
            await this.m_ViewModel.CreateCustomerAsync(this.m_CancellationToken);

            // Assert
            this.m_MockUseCaseInvoker.Verify(mock => mock.InvokeUseCaseAsync(It.IsAny<CreateCustomerRequest>(), this.m_ViewModel, this.m_CancellationToken));
            this.VerifyNoOtherCalls();
        }

        #endregion CreateCustomerAsync Tests

        #region - - - - - - InitialiseAsync Tests - - - - - -

        [Fact]
        public async Task InitialiseAsync_Any_InitialisesGenders()
        {
            // Arrange

            // Act
            await this.m_ViewModel.InitialiseAsync(this.m_CancellationToken);

            // Assert
            this.m_MockUseCaseInvoker.Verify(mock => mock.InvokeUseCaseAsync(It.IsAny<GetGendersRequest>(), this.m_ViewModel.Genders, this.m_CancellationToken));
            this.VerifyNoOtherCalls();
        }

        #endregion InitialiseAsync Tests

        #region - - - - - - PresentAsync Tests - - - - - -

        [Fact]
        public async Task PresentAsync_AnyResponse_MapsToExistingCustomerViewModelAndInvokesOnCustomerCreated()
        {
            // Arrange
            var _CustomerDto = new CustomerDto();
            var _CustomerViewModel = new ExistingCustomerViewModel();

            _ = this.m_MockMapper
                    .Setup(mock => mock.Map<ExistingCustomerViewModel>(_CustomerDto))
                    .Returns(_CustomerViewModel);

            // Act
            await this.m_ViewModel.PresentAsync(_CustomerDto, this.m_CancellationToken);

            // Assert
            this.m_MockOnCustomerCreatedAction.Verify(mock => mock.Invoke(_CustomerViewModel));
            this.VerifyNoOtherCalls();
        }

        #endregion PresentAsync Tests

        #region - - - - - - PresentEntityNotFoundAsync Tests - - - - - -

        [Fact]
        public async Task PresentEntityNotFoundAsync_NotImplemented_ThrowsNotImplementedException()
            => _ = (await Record.ExceptionAsync(() => this.m_ViewModel.PresentEntityNotFoundAsync(new EntityID(), this.m_CancellationToken))).Should().BeOfType<NotImplementedException>();

        #endregion PresentEntityNotFoundAsync Tests

        #region - - - - - - PresentValidationFailureAsync Tests - - - - - -

        [Fact]
        public async Task PresentValidationFailureAsync_AnyValidationResponse_InvokesOnValidationFailure()
        {
            // Arrange
            var _ValidationResult = ValidationResult.Success();

            // Act
            await this.m_ViewModel.PresentValidationFailureAsync(_ValidationResult, this.m_CancellationToken);

            // Assert
            this.m_MockOnValidationFailureAction.Verify(mock => mock.Invoke(_ValidationResult));
            this.VerifyNoOtherCalls();
        }

        #endregion PresentAsync Tests

    }

}
