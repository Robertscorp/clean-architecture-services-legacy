using AutoMapper;
using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Example.Application.UseCases.People.GetGenders;
using CleanArchitecture.Example.InterfaceAdapters.ViewModels.Genders;
using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Pipeline;
using FluentAssertions;
using Moq;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Example.InterfaceAdapters.Tests.Unit.ViewModels.Genders
{

    public class GendersViewModelTests
    {

        #region - - - - - - Fields - - - - - -

        private readonly CancellationToken m_CancellationToken = new CancellationToken();
        private readonly GendersViewModel m_ViewModel;

        private readonly Mock<IMapper> m_MockMapper = new Mock<IMapper>();
        private readonly Mock<IUseCaseInvoker> m_MockUseCaseInvoker = new Mock<IUseCaseInvoker>();

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GendersViewModelTests()
        {
            this.m_ViewModel = new GendersViewModel(this.m_MockMapper.Object, this.m_MockUseCaseInvoker.Object);

            _ = this.m_MockMapper
                    .Setup(mock => mock.ConfigurationProvider)
                    .Returns(new MapperConfiguration(opts => _ = opts.CreateMap<GenderDto, ExistingGenderViewModel>()));
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void VerifyNoOtherCalls()
            => this.m_MockUseCaseInvoker.VerifyNoOtherCalls();

        #endregion Methods

        #region - - - - - - InitialiseAsync Tests - - - - - -

        [Fact]
        public async Task InitialiseAsync_AnyInitialisation_InvokesGetGendersUseCase()
        {
            // Arrange

            // Act
            await this.m_ViewModel.InitialiseAsync(this.m_CancellationToken);

            // Assert
            this.m_MockUseCaseInvoker.Verify(mock => mock.InvokeUseCaseAsync(It.IsAny<GetGendersRequest>(), this.m_ViewModel, this.m_CancellationToken));
            this.VerifyNoOtherCalls();
        }

        #endregion InitialiseAsync Tests

        #region - - - - - - PresentAsync Tests - - - - - -

        [Fact]
        public async Task PresentAsync_AnyQueryable_ProjectsToViewModelsAndAssignsToProperty()
        {
            // Arrange
            var _GenderDtos = new[] { new GenderDto { Name = "Male" } }.AsQueryable();
            var _Expected = new[] { new ExistingGenderViewModel(null, "Male") };

            // Act
            await this.m_ViewModel.PresentAsync(_GenderDtos, this.m_CancellationToken);

            // Assert
            _ = this.m_ViewModel.Genders.Should().BeEquivalentTo(_Expected);
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
        public async Task PresentValidationFailureAsync_NotImplemented_ThrowsNotImplementedException()
            => _ = (await Record.ExceptionAsync(() => this.m_ViewModel.PresentValidationFailureAsync(ValidationResult.Success(), this.m_CancellationToken))).Should().BeOfType<NotImplementedException>();

        #endregion PresentValidationFailureAsync Tests

    }

}
