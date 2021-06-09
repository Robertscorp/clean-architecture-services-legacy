using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.Extended.EFCore
{

    public class PersistenceContext : DbContext, IPersistenceContext
    {

        #region - - - - - - Constructors - - - - - -

        public PersistenceContext(DbContextOptions<PersistenceContext> options) : base(options) { }

        #endregion Constructors

        #region - - - - - - IPersistenceContext Implementation - - - - - -

        Task<EntityID> IPersistenceContext.AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
            => throw new NotImplementedException();

        Task<TEntity> IPersistenceContext.FindAsync<TEntity>(EntityID entityID, CancellationToken cancellationToken)
            => throw new NotImplementedException();

        Task<IQueryable<TEntity>> IPersistenceContext.GetEntitiesAsync<TEntity>(CancellationToken cancellationToken)
            => Task.FromResult((IQueryable<TEntity>)new QueryableUnion<TEntity>(
                this.ChangeTracker.Entries<TEntity>().Where(e => e.State == EntityState.Added).Select(e => e.Entity).AsQueryable(),
                this.Set<TEntity>()));

        Task IPersistenceContext.RemoveAsync<TEntity>(TEntity entity, CancellationToken cancellationToken)
            => throw new NotImplementedException();

        #endregion IPersistenceContext Implementation

    }

    public class QueryableUnion<TEntity> : IQueryable<TEntity>, IOrderedQueryable<TEntity>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IQueryable<TEntity>[] m_Queries;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public QueryableUnion(params IQueryable<TEntity>[] queries)
        {
            if (queries is null)
                throw new ArgumentNullException(nameof(queries));

            if (queries.Length < 2)
                throw new ArgumentException("Must provide at least 2 queries to union.", nameof(queries));

            this.m_Queries = queries;
        }

        #endregion Constructors

        #region - - - - - - IQueryable Implementation - - - - - -

        public Type ElementType
            => this.m_Queries[0].ElementType;

        public Expression Expression
            => this.m_Queries[0].Expression;

        public IQueryProvider Provider
            => new QueryableUnionQueryProvider(this.m_Queries);

        public IEnumerator<TEntity> GetEnumerator()
            => new QueryableUnionEnumerator(this.m_Queries);

        IEnumerator IEnumerable.GetEnumerator()
            => ((IQueryable<TEntity>)this).GetEnumerator();

        #endregion IQueryable Implementation

        #region - - - - - - Nested Classes - - - - - -

        private class QueryableUnionQueryProvider : IQueryProvider
        {

            #region - - - - - - Fields - - - - - -

            private readonly IQueryable<TEntity>[] m_Queries;

            #endregion Fields

            #region - - - - - - Constructors - - - - - -

            public QueryableUnionQueryProvider(IQueryable<TEntity>[] queries)
                => this.m_Queries = queries;

            #endregion Constructors

            #region - - - - - - IQueryProvider Implementation - - - - - -

            public IQueryable CreateQuery(Expression expression)
                => throw new NotImplementedException();

            public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
                => new QueryableUnion<TElement>(
                    this.m_Queries.Select(q =>
                    {
                        var _Expression = (MethodCallExpression)expression;
                        var _QueryExpression = Expression.Call(
                                                _Expression.Object,
                                                _Expression.Method,
                                                new[] { q.Expression }.Union(_Expression.Arguments.Skip(1)).ToArray());

                        return q.Provider.CreateQuery<TElement>(_QueryExpression);
                    }).ToArray());

            public object Execute(Expression expression)
                => throw new NotImplementedException();

            public TResult Execute<TResult>(Expression expression)
                => throw new NotImplementedException();

            #endregion IQueryProvider Implementation

        }

        private class QueryableUnionEnumerator : IEnumerator<TEntity>
        {

            #region - - - - - - Fields - - - - - -

            private readonly IEnumerator<IEnumerator<TEntity>> m_EnumeratorEnumerator;
            private readonly List<IEnumerator<TEntity>> m_QueryEnumerators;

            #endregion Fields

            #region - - - - - - Constructors - - - - - -

            public QueryableUnionEnumerator(IQueryable<TEntity>[] queries)
            {
                this.m_QueryEnumerators = queries.Select(q => q.GetEnumerator()).ToList();
                this.m_EnumeratorEnumerator = this.m_QueryEnumerators.GetEnumerator();
                _ = this.m_EnumeratorEnumerator.MoveNext(); // Handle here so we don't need to handle initial case in every call to MoveNext.
            }

            #endregion Constructors

            #region - - - - - - IEnumerator Implementation - - - - - -

            public TEntity Current
                => this.m_EnumeratorEnumerator.Current == null
                    ? default
                    : this.m_EnumeratorEnumerator.Current.Current;

            object IEnumerator.Current
                => ((IEnumerator<TEntity>)this).Current;

            public void Dispose()
            {
                foreach (var _Enumerator in this.m_QueryEnumerators)
                    _Enumerator.Dispose();

                this.m_EnumeratorEnumerator.Dispose();
            }

            public bool MoveNext()
                => this.m_EnumeratorEnumerator.Current.MoveNext() || (this.m_EnumeratorEnumerator.MoveNext() && this.MoveNext()); // When reaching the end of a query, move to the beginning of the next query.

            public void Reset()
            {
                foreach (var _Enumerator in this.m_QueryEnumerators)
                    _Enumerator.Reset();

                this.m_EnumeratorEnumerator.Reset();
                _ = this.m_EnumeratorEnumerator.MoveNext(); // Handle here so we don't need to handle initial case in every call to MoveNext.
            }

            #endregion IEnumerator Implementation

        }

        #endregion Nested Classes

    }

}
