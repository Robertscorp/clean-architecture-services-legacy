using System;
using System.Linq.Expressions;
using System.Reflection;

namespace CleanArchitecture.Services.Extensions
{

    public static class PropertyInfoExtensions
    {

        #region - - - - - - Methods - - - - - -

        public static Func<TInputType, TResultType> AsFunction<TInputType, TResultType>(this PropertyInfo propertyInfo)
        {
            var _InputParameter = Expression.Parameter(typeof(TInputType));
            var _Input = Equals(typeof(TInputType), propertyInfo.DeclaringType)
                            ? (Expression)_InputParameter
                            : Expression.Convert(_InputParameter, propertyInfo.DeclaringType);

            var _Property = Expression.Property(_Input, propertyInfo);
            var _Body = Equals(typeof(TResultType), propertyInfo.PropertyType)
                            ? (Expression)_Property
                            : Expression.Convert(_Property, typeof(TResultType));

            return Expression.Lambda<Func<TInputType, TResultType>>(_Body, _InputParameter).Compile();
        }

        #endregion Methods

    }

}
