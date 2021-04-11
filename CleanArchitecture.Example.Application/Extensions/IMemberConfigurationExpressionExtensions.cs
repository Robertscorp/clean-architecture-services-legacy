using AutoMapper;
using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Enumerations;
using CleanArchitecture.Services.Persistence;
using System;
using System.Linq.Expressions;
using System.Threading;

namespace CleanArchitecture.Example.Application.Extensions
{

    public static class IMemberConfigurationExpressionExtensions
    {

        #region - - - - - - Methods - - - - - -

        public static void MapFromEntity<TSource, TDestination, TSourceMember>(
            this IMemberConfigurationExpression<TSource, TDestination, EntityID> memberConfigurationExpression,
            Expression<Func<TSource, TSourceMember>> sourceMember)
            where TSourceMember : class
            => memberConfigurationExpression.ConvertUsing<EntityToEntityIDConverter<TSourceMember>, TSourceMember>(sourceMember);

        public static void MapFromEnumeration<TSource, TDestination, TSourceMember>(
            this IMemberConfigurationExpression<TSource, TDestination, EntityID> memberConfigurationExpression,
            Func<TSource, TSourceMember> sourceMember)
            where TSourceMember : Enumeration
            => memberConfigurationExpression.MapFrom(src => sourceMember.Invoke(src).ToEntityID());

        public static void MapFromEntityID<TSource, TDestination, TDestinationMember>(
            this IMemberConfigurationExpression<TSource, TDestination, TDestinationMember> memberConfigurationExpression,
            Func<TSource, EntityID> sourceMemberFunction)
            where TDestinationMember : Enumeration
            => memberConfigurationExpression.MapFrom(src => Enumeration.FromEntityID<TDestinationMember>(sourceMemberFunction.Invoke(src)));

        #endregion Methods

        #region - - - - - - Nested Classes - - - - - -

        private class EntityToEntityIDConverter<TEntity> : IValueConverter<TEntity, EntityID> where TEntity : class
        {

            #region - - - - - - Fields - - - - - -

            private readonly IPersistenceContext m_PersistenceContext;

            #endregion Fields

            #region - - - - - - Constructors - - - - - -

            public EntityToEntityIDConverter(IPersistenceContext persistenceContext)
                => this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));

            #endregion Constructors

            #region - - - - - - IValueConverter Implementation - - - - - -

            public EntityID Convert(TEntity sourceMember, ResolutionContext context)
                => this.m_PersistenceContext.GetEntityIDAsync(sourceMember, CancellationToken.None).GetAwaiter().GetResult();

            #endregion IValueConverter Implementation

        }

        #endregion Nested Classes

    }

}
