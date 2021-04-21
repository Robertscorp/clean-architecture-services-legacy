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

        private CancellationToken m_CancellationToken = new CancellationToken();

        private Mock<IMapper> m_MockMapper = new Mock<IMapper>();
        private Mock<IUseCaseInvoker> m_MockUseCaseInvoker = new Mock<IUseCaseInvoker>();

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GendersViewModelTests()
            => _ = this.m_MockMapper
                    .Setup(mock => mock.ConfigurationProvider)
                    .Returns(new MapperConfiguration(opts => _ = opts.CreateMap<GenderDto, ExistingGenderViewModel>()));

        #endregion Constructors

        #region - - - - - - InitialiseAsync Tests - - - - - -

        [Fact]
        public async Task InitialiseAsync_AnyInitialisation_InvokesGetGendersUseCase()
        {
            // Arrange
            var _GendersViewModel = new GendersViewModel(this.m_MockMapper.Object, this.m_MockUseCaseInvoker.Object);

            // Act
            await _GendersViewModel.InitialiseAsync(this.m_CancellationToken);

            // Assert
            this.m_MockUseCaseInvoker.Verify(mock => mock.InvokeUseCaseAsync(It.IsAny<GetGendersRequest>(), _GendersViewModel, this.m_CancellationToken));

            this.m_MockMapper.VerifyNoOtherCalls();
            this.m_MockUseCaseInvoker.VerifyNoOtherCalls();
        }

        #endregion InitialiseAsync Tests

        #region - - - - - - PresentAsync Tests - - - - - -

        [Fact]
        public async Task PresentAsync_AnyQueryable_ProjectsToViewModelsAndAssignsToProperty()
        {
            // Arrange
            var _GenderDtos = new[] { new GenderDto { Name = "Male" } }.AsQueryable();

            var _GendersViewModel = new GendersViewModel(this.m_MockMapper.Object, this.m_MockUseCaseInvoker.Object);

            var _Expected = new[] { new ExistingGenderViewModel(null, "Male") };

            // Act
            await _GendersViewModel.PresentAsync(_GenderDtos, this.m_CancellationToken);

            // Assert
            _ = _GendersViewModel.Genders.Should().BeEquivalentTo(_Expected);

            this.m_MockMapper.Verify(mock => mock.ConfigurationProvider);
            this.m_MockMapper.VerifyNoOtherCalls();
            this.m_MockUseCaseInvoker.VerifyNoOtherCalls();
        }

        #endregion PresentAsync Tests

        #region - - - - - - PresentEntityNotFoundAsync Tests - - - - - -

        [Fact]
        public async Task PresentEntityNotFoundAsync_NotImplemented_ThrowsNotImplementedException()
        {
            // Arrange
            var _GendersViewModel = new GendersViewModel(this.m_MockMapper.Object, this.m_MockUseCaseInvoker.Object);

            // Act
            var _Exception = await Record.ExceptionAsync(() => _GendersViewModel.PresentEntityNotFoundAsync(new EntityID(), this.m_CancellationToken));

            // Assert
            _ = _Exception.Should().BeOfType<NotImplementedException>();

            this.m_MockMapper.VerifyNoOtherCalls();
            this.m_MockUseCaseInvoker.VerifyNoOtherCalls();
        }

        #endregion PresentEntityNotFoundAsync Tests

        #region - - - - - - PresentValidationFailureAsync Tests - - - - - -

        [Fact]
        public async Task PresentValidationFailureAsync_NotImplemented_ThrowsNotImplementedException()
        {
            // Arrange
            var _GendersViewModel = new GendersViewModel(this.m_MockMapper.Object, this.m_MockUseCaseInvoker.Object);

            // Act
            var _Exception = await Record.ExceptionAsync(() => _GendersViewModel.PresentValidationFailureAsync(ValidationResult.Success(), this.m_CancellationToken));

            // Assert
            _ = _Exception.Should().BeOfType<NotImplementedException>();

            this.m_MockMapper.VerifyNoOtherCalls();
            this.m_MockUseCaseInvoker.VerifyNoOtherCalls();
        }

        #endregion PresentValidationFailureAsync Tests

    }

}
