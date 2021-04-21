using CleanArchitecture.Example.InterfaceAdapters.Entities;
using FluentAssertions;
using System;
using Xunit;

namespace CleanArchitecture.Example.InterfaceAdapters.Tests.Unit.Entities
{

    public class PropertyTests
    {

        #region - - - - - - Fields - - - - - -

        private string m_Actual;
        private Property<string> m_Property = new Property<string>();
        private bool m_WasInvoked;

        #endregion Fields

        #region - - - - - - ValueChanged Tests - - - - - -

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("abc")]
        public void ValueChanged_PropertyHasExistingValueAssigned_ValueChangedIsNotInvoked(string value)
        {
            // Arrange
            this.m_Property.Value = value;
            this.m_Property.ValueChanged = val => throw new Exception("Should not be invoked in this test.");

            // Act
            this.m_Property.Value = value;

            // Assert
            _ = this.m_Actual.Should().Be(null);
            _ = this.m_WasInvoked.Should().BeFalse();
        }

        [Theory]
        [InlineData(null, "")]
        [InlineData("", null)]
        [InlineData("abc", "ABC")]
        public void ValueChanged_PropertyHasNewValueAssigned_ValueChangedIsInvoked(string oldValue, string newValue)
        {
            // Arrange
            this.m_Property.Value = oldValue;
            this.m_Property.ValueChanged = val =>
            {
                this.m_Actual = val;
                this.m_WasInvoked = true;
            };

            // Act
            this.m_Property.Value = newValue;

            // Assert
            _ = this.m_Actual.Should().Be(newValue);
            _ = this.m_WasInvoked.Should().BeTrue();
        }

        #endregion ValueChanged Tests

    }

}
