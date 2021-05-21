using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Extended.AutoMapper;
using CleanArchitecture.Services.Persistence;
using FluentAssertions;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace CleanArchitecture.Services.Extended.Tests.Unit.AutoMapper
{

    public class EntityIDConverterTests
    {

        #region - - - - - - Convert - - - - - -

        [Fact]
        public void Convert_AnyRequest_GetsResultFromPersistenceContext()
        {
            // Arrange
            var _EntityID = new Mock<EntityID>().Object;
            var _Expected = new Mock<IEntity>().Object;

            var _MockPersistenceContext = new Mock<IPersistenceContext>();
            _ = _MockPersistenceContext
                    .Setup(mock => mock.FindAsync<IEntity>(_EntityID, It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(_Expected));

            var _Converter = new EntityIDConverter<IEntity>(_MockPersistenceContext.Object);

            // Act
            var _Actual = _Converter.Convert(_EntityID, null);

            // Assert
            _ = _Actual.Should().Be(_Expected);
        }

        #endregion Convert

    }

}
