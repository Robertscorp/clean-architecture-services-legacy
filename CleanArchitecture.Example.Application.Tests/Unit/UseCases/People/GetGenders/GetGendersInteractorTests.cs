using AutoMapper;
using CleanArchitecture.Example.Application.UseCases.People.GetGenders;
using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Extended.Pipeline;
using CleanArchitecture.Services.Persistence;
using FluentAssertions;
using Moq;
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
            var _Actual = default(IQueryable<GenderDto>);
            var _CancellationToken = new CancellationToken();
            var _GenderDtos = new[] { new GenderDto { Name = Gender.Mayonnaise.Name } };
            var _Genders = new[] { Gender.Mayonnaise };

            var _MockMapper = new Mock<IMapper>();
            _ = _MockMapper
                    .Setup(mock => mock.ConfigurationProvider)
                    .Returns(new MapperConfiguration(opts => opts.CreateMap<Gender, GenderDto>()));

            var _MockPersistenceContext = new Mock<IPersistenceContext>();
            _ = _MockPersistenceContext
                    .Setup(mock => mock.GetEntitiesAsync<Gender>(_CancellationToken))
                    .Returns(Task.FromResult(_Genders.AsQueryable()));

            var _MockPresenter = new Mock<IPresenter<IQueryable<GenderDto>>>();
            _ = _MockPresenter
                    .Setup(mock => mock.PresentAsync(It.IsAny<IQueryable<GenderDto>>(), _CancellationToken))
                    .Callback((IQueryable<GenderDto> dtos, CancellationToken ct) => _Actual = dtos);

            var _Interactor = new GetGendersInteractor(_MockMapper.Object, _MockPersistenceContext.Object);

            // Act
            await _Interactor.HandleAsync(new GetGendersRequest(), _MockPresenter.Object, _CancellationToken);

            // Assert
            _ = _Actual.Should().BeEquivalentTo(_GenderDtos);

            _MockPersistenceContext.Verify(mock => mock.GetEntitiesAsync<Gender>(_CancellationToken));
            _MockPresenter.Verify(mock => mock.PresentAsync(It.IsAny<IQueryable<GenderDto>>(), _CancellationToken));

            _MockPresenter.VerifyNoOtherCalls();
        }

        #endregion HandleAsync Tests

    }

}
