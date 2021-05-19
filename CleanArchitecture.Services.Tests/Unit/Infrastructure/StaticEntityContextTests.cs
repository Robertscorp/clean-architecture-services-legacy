using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Infrastructure;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Services.Tests.Unit.Infrastructure
{

    public class StaticEntityContextTests
    {

        #region - - - - - - Find Tests - - - - - -

        [Fact]
        public void Find_EntityIDIsFound_ReturnsStaticEntity()
        {
            // Arrange
            var _Expected = DayOfWeek.Tuesday;

            // Act
            var _Actual = StaticEntityContext.Find<DayOfWeek>(DayOfWeek.Tuesday.ID);

            // Assert
            _ = _Actual.Should().Be(_Expected);
        }

        [Fact]
        public void Find_EntityIDIsNotFound_ReturnsNull()
        {
            // Arrange

            // Act
            var _Actual = StaticEntityContext.Find<DayOfWeek>(new TestEntityID());

            // Assert
            _ = _Actual.Should().BeNull();
        }

        [Fact]
        public void Find_EntityIsNotAContextEntity_ReturnsNull()
        {
            // Arrange

            // Act
            var _Actual = StaticEntityContext.Find<DayOfWeek>(DayOfWeek.Smunday.ID);

            // Assert
            _ = _Actual.Should().BeNull();
        }

        [Fact]
        public void Find_EntityTypeIsNotAStaticEntity_ReturnsNull()
        {
            // Arrange

            // Act
            var _Actual = StaticEntityContext.Find<NotStaticEntity>(new TestEntityID());

            // Assert
            _ = _Actual.Should().BeNull();
        }

        #endregion Find Tests

        #region - - - - - - GetEntities Tests - - - - - -

        [Fact]
        public void GetEntities_EntityIsAStaticEntity_ReturnsAllContextEntities()
        {
            // Arrange
            var _Expected = new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday };

            // Act
            var _Actual = StaticEntityContext.GetEntities<DayOfWeek>();

            // Assert
            _ = _Actual.Should().BeEquivalentTo(_Expected);
        }

        [Fact]
        public void GetEntities_EntityIsNotAStaticEntity_ReturnsNull()
        {
            // Arrange

            // Act
            var _Actual = StaticEntityContext.GetEntities<NotStaticEntity>();

            // Assert
            _ = _Actual.Should().BeNull();
        }

        #endregion GetEntities Tests

        #region - - - - - - Nested Classes - - - - - -

        private class DayOfWeek : StaticEntity
        {

            #region - - - - - - Fields - - - - - -

            public static DayOfWeek Monday = new("Monday", 1);
            public static DayOfWeek Smunday = new("Smunday", 8);
            public static DayOfWeek Tuesday = new("Tuesday", 2);
            public static DayOfWeek Wednesday = new("Wednesday", 3);

            #endregion Fields

            #region - - - - - - Constructors - - - - - -

            private DayOfWeek(string name, long value) : base(name, value) { }

            #endregion Constructors

            #region - - - - - - Methods - - - - - -

            protected override bool IsContextEntity()
                => !Equals(this, Smunday);

            #endregion Methods

        }

        private class NotStaticEntity
        {

            #region - - - - - - Fields - - - - - -

            public static NotStaticEntity StaticInstance = new();

            #endregion Fields

            #region - - - - - - Constructors - - - - - -

            private NotStaticEntity() { }

            #endregion Constructors

        }

        private class TestEntityID : EntityID { }

        #endregion Nested Classes

    }

}
