using AutoMapper;
using CleanArchitecture.Services.Entities;
using System;

namespace CleanArchitecture.Example.Application.Extensions
{

    public static class IMemberConfigurationExpressionExtensions
    {

        #region - - - - - - Methods - - - - - -

        public static void MapFromEntityID<TSource, TDestination, TDestinationMember>(
            this IMemberConfigurationExpression<TSource, TDestination, TDestinationMember> memberConfigurationExpression,
            Func<TSource, EntityID> sourceMemberFunction)
            where TDestinationMember : StaticEntity
            => memberConfigurationExpression.MapFrom(src => StaticEntity.Get<TDestinationMember>(sourceMemberFunction.Invoke(src)));

        #endregion Methods

    }

}
