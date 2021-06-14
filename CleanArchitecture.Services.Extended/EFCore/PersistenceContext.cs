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

    public class QueryableUnion<TElement> : IQueryable<TElement>, IQueryProvider, IOrderedQueryable<TElement>
    {

        #region - - - - - - Fields - - - - - -

        private readonly (IQueryable<TElement> Query, ExpressionVisitor Visitor)[] m_QueriesWithVisitors;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public QueryableUnion(params IQueryable<TElement>[] queries)
        {
            if (queries is null)
                throw new ArgumentNullException(nameof(queries));

            if (queries.Length < 2)
                throw new ArgumentException("Must provide at least 2 queries to union.", nameof(queries));

            this.Expression = Expression.Constant(this);
            this.m_QueriesWithVisitors = queries.Select(q => (q, (ExpressionVisitor)new QueryableUnionVisitor(this, q))).ToArray();
        }

        private QueryableUnion(Expression expression, (IQueryable<TElement>, ExpressionVisitor)[] queriesWithVisitors)
        {
            this.Expression = expression;
            this.m_QueriesWithVisitors = queriesWithVisitors;
        }

        #endregion Constructors

        #region - - - - - - IQueryable Implementation - - - - - -

        public Type ElementType
            => typeof(TElement);

        public Expression Expression { get; }

        public IQueryProvider Provider
            => this;

        public IEnumerator<TElement> GetEnumerator()
            => new QueryableUnionEnumerator(this.m_QueriesWithVisitors.Select(qv => qv.Query).ToArray());

        IEnumerator IEnumerable.GetEnumerator()
            => ((IQueryable<TElement>)this).GetEnumerator();

        #endregion IQueryable Implementation

        #region - - - - - - IQueryProvider Implementation - - - - - -

        public IQueryable CreateQuery(Expression expression)
            => (IQueryable)Activator.CreateInstance(
                typeof(QueryableUnion<>).MakeGenericType(expression.Type.GenericTypeArguments.First()),
                this.m_QueriesWithVisitors.Select(qv => (qv.Query.Provider.CreateQuery(qv.Visitor.Visit(expression)), qv.Visitor)).ToArray());

        public IQueryable<T> CreateQuery<T>(Expression expression)
            => new QueryableUnion<T>(expression, this.m_QueriesWithVisitors.Select(qv => (qv.Query.Provider.CreateQuery<T>(qv.Visitor.Visit(expression)), qv.Visitor)).ToArray());


        public object Execute(Expression expression)
            => throw new NotImplementedException();

        public TResult Execute<TResult>(Expression expression)
            => throw new NotImplementedException();

        #endregion IQueryProvider Implementation

        #region - - - - - - Nested Classes - - - - - -

        private class QueryableUnionEnumerator : IEnumerator<TElement>
        {

            #region - - - - - - Fields - - - - - -

            private readonly IEnumerator<IEnumerator<TElement>> m_EnumeratorEnumerator;
            private readonly List<IEnumerator<TElement>> m_QueryEnumerators;

            #endregion Fields

            #region - - - - - - Constructors - - - - - -

            public QueryableUnionEnumerator(IQueryable<TElement>[] queries)
            {
                this.m_QueryEnumerators = queries.Select(q => q.GetEnumerator()).ToList();
                this.m_EnumeratorEnumerator = this.m_QueryEnumerators.GetEnumerator();
                _ = this.m_EnumeratorEnumerator.MoveNext(); // Handle here so we don't need to handle initial case in every call to MoveNext.
            }

            #endregion Constructors

            #region - - - - - - IEnumerator Implementation - - - - - -

            public TElement Current
                => this.m_EnumeratorEnumerator.Current == null
                    ? default
                    : this.m_EnumeratorEnumerator.Current.Current;

            object IEnumerator.Current
                => ((IEnumerator<TElement>)this).Current;

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

        private class QueryableUnionVisitor : ExpressionVisitor
        {

            #region - - - - - - Fields - - - - - -

            private readonly IQueryable m_OriginalQuery;
            private readonly IQueryable m_ReplacementQuery;

            #endregion Fields

            #region - - - - - - Constructors - - - - - -

            public QueryableUnionVisitor(IQueryable originalQuery, IQueryable replacementQuery)
            {
                this.m_OriginalQuery = originalQuery;
                this.m_ReplacementQuery = replacementQuery;
            }

            #endregion Constructors

            #region - - - - - - Methods - - - - - -

            protected override Expression VisitConstant(ConstantExpression node)
                => Equals(node.Value, this.m_OriginalQuery)
                    ? Expression.Constant(this.m_ReplacementQuery)
                    : base.VisitConstant(node);

            #endregion Methods

        }

        #endregion Nested Classes

    }

}
