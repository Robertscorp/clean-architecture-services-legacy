using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Enumerations;
using FluentAssertions;
using Xunit;

namespace CleanArchitecture.Services.Tests.Unit.Enumerations
{

    public class EnumerationTests
    {

        #region - - - - - - FromEntityID Tests - - - - - -

        [Fact]
        public void FromEntityID_EntityIDIsNotAnEnumerationEntityID_ReturnsNull()
        {
            // Arrange
            var _EntityID = new EntityID();

            // Act
            var _Actual = Enumeration.FromEntityID<ShapeEnumeration>(_EntityID);

            // Assert
            _ = _Actual.Should().BeNull();
        }

        [Fact]
        public void FromEntityID_EntityIDIsForADifferentEnumeration_ReturnsNull()
        {
            // Arrange
            var _EntityID = DayEnumeration.Monday.ToEntityID();

            // Act
            var _Actual = Enumeration.FromEntityID<ShapeEnumeration>(_EntityID);

            // Assert
            _ = _Actual.Should().BeNull();
        }

        [Fact]
        public void FromEntityID_EntityIDIsValid_ReturnsEnumerationValue()
        {
            // Arrange
            var _EntityID = ShapeEnumeration.Circle.ToEntityID();

            // Act
            var _Actual = Enumeration.FromEntityID<ShapeEnumeration>(_EntityID);

            // Assert
            _ = _Actual.Should().Be(ShapeEnumeration.Circle);
        }

        #endregion FromEntityID Tests

        #region - - - - - - Nested Classes - - - - - -

        private class ShapeEnumeration : Enumeration
        {

            #region - - - - - - Fields - - - - - -

            public static readonly ShapeEnumeration Circle = new ShapeEnumeration("Circle", 1);
            public static readonly ShapeEnumeration Square = new ShapeEnumeration("Square", 2);

            #endregion Fields

            #region - - - - - - Constructors - - - - - -

            private ShapeEnumeration(string name, int value) : base(name, value) { }

            #endregion Constructors

        }

        private class DayEnumeration : Enumeration
        {

            #region - - - - - - Fields - - - - - -

            public static readonly DayEnumeration Monday = new DayEnumeration("Monday", 1);
            public static readonly DayEnumeration Tuesday = new DayEnumeration("Tuesday", 2);

            #endregion Fields

            #region - - - - - - Constructors - - - - - -

            private DayEnumeration(string name, int value) : base(name, value) { }

            #endregion Constructors

        }

        #endregion Nested Classes

    }

}
