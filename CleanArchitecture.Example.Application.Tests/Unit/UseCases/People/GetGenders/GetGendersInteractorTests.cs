using AutoMapper;
using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Example.Application.UseCases.People.GetGenders;
using CleanArchitecture.Example.Domain.Enumerations;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Example.Application.Tests.Unit.UseCases.People.GetGenders
{

    public class GetGendersInteractorTests
    {

        #region - - - - - - HandleAsync Tests - - - - - -

        [Fact]
        public async Task HandleAsync_AnyRequest_PresentsSuccessfully()
        {
            // Arrange
            var _CancellationToken = new CancellationToken();
            var _GenderDtos = new List<GenderDto>();

            var _MockMapper = new Mock<IMapper>();
            _MockMapper
                .Setup(mock => mock.Map<List<GenderDto>>(It.IsAny<IEnumerable<GenderEnumeration>>()))
                .Returns(_GenderDtos);

            var _MockPresenter = new Mock<IPresenter<IQueryable<GenderDto>>>();

            var _Interactor = new GetGendersInteractor(_MockMapper.Object);

            // Act
            await _Interactor.HandleAsync(new GetGendersRequest(), _MockPresenter.Object, _CancellationToken);

            // Assert
            _MockMapper.Verify(mock => mock.Map<List<GenderDto>>(It.IsAny<IEnumerable<GenderEnumeration>>()));
            _MockPresenter.Verify(mock => mock.PresentAsync(It.IsAny<IQueryable<GenderDto>>(), _CancellationToken));

            _MockMapper.VerifyNoOtherCalls();
            _MockPresenter.VerifyNoOtherCalls();
        }

        #endregion HandleAsync Tests

    }

}
