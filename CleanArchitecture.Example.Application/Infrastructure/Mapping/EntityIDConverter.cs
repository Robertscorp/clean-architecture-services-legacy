using AutoMapper;
using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Persistence;
using System;
using System.Threading;

namespace CleanArchitecture.Example.Application.Infrastructure.Mapping
{

    public class EntityIDConverter<TDestinationMember> : IValueConverter<EntityID, TDestinationMember> where TDestinationMember : class
    {

        #region - - - - - - Fields - - - - - -

        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public EntityIDConverter(IPersistenceContext persistenceContext)
            => this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));

        #endregion Constructors

        #region - - - - - - IValueConverter Implementation - - - - - -

        public TDestinationMember Convert(EntityID sourceMember, ResolutionContext context)
            => this.m_PersistenceContext.FindAsync<TDestinationMember>(sourceMember, CancellationToken.None).GetAwaiter().GetResult();

        #endregion IValueConverter Implementation

    }

}
