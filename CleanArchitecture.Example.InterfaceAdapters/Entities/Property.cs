using System;

namespace CleanArchitecture.Example.InterfaceAdapters.Entities
{

    public class Property<TProperty>
    {

        #region - - - - - - Fields - - - - - -

        private TProperty m_Value;

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public TProperty Value
        {
            get => this.m_Value;
            set
            {
                if (!object.Equals(this.m_Value, value))
                {
                    this.m_Value = value;
                    this.ValueChanged?.Invoke(value);
                }
            }
        }

        public Action<TProperty> ValueChanged { get; set; }

        #endregion Properties

    }

}
