using AutoMapper;
using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Enumerations;
using System;
using System.Linq.Expressions;

namespace CleanArchitecture.Example.Application.Extensions
{

    public static class IMemberConfigurationExpressionExtensions
    {

        #region - - - - - - Methods - - - - - -

        public static void MapFromEntityID<TSource, TDestination, TDestinationMember>(
            this IMemberConfigurationExpression<TSource, TDestination, TDestinationMember> memberConfigurationExpression,
            Expression<Func<TSource, EntityID>> sourceMember)
            where TDestinationMember : Enumeration
            => memberConfigurationExpression.MapFrom(new EnumerationValueResolver<TSource, TDestination, TDestinationMember>(), sourceMember);

        #endregion Methods

        #region - - - - - - Nested Classes - - - - - -

        private class EnumerationValueResolver<TSource, TDestination, TDestinationMember> : IMemberValueResolver<TSource, TDestination, EntityID, TDestinationMember> where TDestinationMember : Enumeration
        {

            #region - - - - - - IMemberValueResolver Implementation - - - - - -

            public TDestinationMember Resolve(TSource source, TDestination destination, EntityID sourceMember, TDestinationMember destMember, ResolutionContext context)
                => Enumeration.FromEntityID<TDestinationMember>(sourceMember);

            #endregion IMemberValueResolver Implementation

        }

        #endregion Nested Classes

    }

}
