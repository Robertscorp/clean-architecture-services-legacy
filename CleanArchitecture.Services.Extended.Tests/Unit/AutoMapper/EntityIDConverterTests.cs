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

        [Theory]
        [InlineData(null)]
        [InlineData("Expected")]
        public void Convert_AnyRequest_GetsResultFromPersistenceContext(object expected)
        {
            // Arrange
            var _EntityID = new Mock<EntityID>().Object;

            var _MockPersistenceContext = new Mock<IPersistenceContext>();
            _ = _MockPersistenceContext
                    .Setup(mock => mock.FindAsync<object>(_EntityID, It.IsAny<CancellationToken>()))
                    .Returns(Task.FromResult(expected));

            var _Converter = new EntityIDConverter<object>(_MockPersistenceContext.Object);

            // Act
            var _Actual = _Converter.Convert(_EntityID, null);

            // Assert
            _ = _Actual.Should().Be(expected);
        }

        #endregion Convert

    }

}
