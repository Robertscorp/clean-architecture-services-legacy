using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CleanArchitecture.Services.Extended.Queryable
{

    public class ConcatenatedQueryable<TElement> : IQueryable<TElement>, IQueryProvider, IOrderedQueryable<TElement>
    {

        #region - - - - - - Constructors - - - - - -

        public ConcatenatedQueryable(IQueryable<TElement> firstQuery, IQueryable<TElement> secondQuery)
            => this.Expression = new ConcatenationExpression(firstQuery, secondQuery);

        private ConcatenatedQueryable(Expression expression)
            => this.Expression = expression;

        #endregion Constructors

        #region - - - - - - IQueryable Implementation - - - - - -

        public Type ElementType
            => typeof(TElement);

        public Expression Expression { get; }

        public IQueryProvider Provider
            => this;

        public IEnumerator<TElement> GetEnumerator()
        {
            return null;

            //var _X = new ConcatenatedQueryableExpressionVisitor().Visit(this.Expression);
            //var _Y = new ReduceExpressionVisitor().Visit(_X);

            //return Expression
            //    .Lambda<Func<IQueryable<TElement>>>(
            //        Expression.Convert(
            //            _Y,
            //            typeof(IQueryable<>).MakeGenericType(this.ElementType)))
            //    .Compile()
            //    .Invoke()
            //    .GetEnumerator();
        }
        //=> Expression
        //    .Lambda<Func<IQueryable<TElement>>>(
        //        Expression.Convert(
        //            new ConcatenatedQueryableExpressionVisitor().Visit(this.Expression).Reduce(),
        //            typeof(IQueryable<>).MakeGenericType(this.ElementType)))
        //    .Compile()
        //    .Invoke()
        //    .GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
            => ((IQueryable<TElement>)this).GetEnumerator();

        #endregion IQueryable Implementation

        #region - - - - - - IQueryProvider Implementation - - - - - -

        public IQueryable CreateQuery(Expression expression)
            => (IQueryable)Activator.CreateInstance(typeof(ConcatenatedQueryable<>).MakeGenericType(expression.Type.GenericTypeArguments[0]), new XVisitor().Visit(expression));

        public IQueryable<T> CreateQuery<T>(Expression expression)
            => new ConcatenatedQueryable<T>(new XVisitor().Visit(expression));

        public object Execute(Expression expression)
            => throw new NotImplementedException();

        public TResult Execute<TResult>(Expression expression)
            => throw new NotImplementedException();

        #endregion IQueryProvider Implementation

    }


    public class XVisitor : ExpressionVisitor
    {

        #region - - - - - - Methods - - - - - -

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var _Arguments = this.Visit(node.Arguments);

            xxx // Visiting a node with children will reduce them...

            var _Concatenation = _Arguments.OfType<ConcatenationExpression>().SingleOrDefault();
            if (_Concatenation == null)
                return base.VisitMethodCall(node);

            var _Object = this.Visit(node.Object);
            var _ConcatenationIndex = _Arguments.IndexOf(_Concatenation);

            var _FirstArgumentArray = _Arguments.ToArray();
            _FirstArgumentArray[_ConcatenationIndex] = _Concatenation.First;

            var _SecondArgumentArray = _Arguments.ToArray();
            _SecondArgumentArray[_ConcatenationIndex] = _Concatenation.Second;

            return new ConcatenationExpression(
                Expression.Call(_Object, node.Method, _FirstArgumentArray),
                Expression.Call(_Object, node.Method, _SecondArgumentArray));
        }

        #endregion Methods

    }


    //public class ConcatenatedQueryableExpressionVisitor : ExpressionVisitor
    //{

    //    #region - - - - - - Methods - - - - - -

    //    [return: NotNullIfNotNull("node")]
    //    public override Expression Visit(Expression node)
    //        => node is ConcatenationExpression _Node
    //            ? new DuplicateTreeExpression(this.Visit(_Node.FirstQuery.Expression), this.Visit(_Node.SecondQuery.Expression))
    //            : base.Visit(node);

    //    protected override Expression VisitMethodCall(MethodCallExpression node)
    //    {
    //        var _Arguments = this.Visit(node.Arguments);

    //        var _DuplicateTree = _Arguments.OfType<DuplicateTreeExpression>().SingleOrDefault();
    //        if (_DuplicateTree == null)
    //            return base.VisitMethodCall(node);

    //        var _Object = this.Visit(node.Object);
    //        var _DuplicateTreeIndex = _Arguments.IndexOf(_DuplicateTree);

    //        var _FirstArgumentArray = _Arguments.ToArray();
    //        _FirstArgumentArray[_DuplicateTreeIndex] = _DuplicateTree.First;

    //        var _SecondArgumentArray = _Arguments.ToArray();
    //        _SecondArgumentArray[_DuplicateTreeIndex] = _DuplicateTree.Second;

    //        return new DuplicateTreeExpression(
    //            Expression.Call(_Object, node.Method, _FirstArgumentArray),
    //            Expression.Call(_Object, node.Method, _SecondArgumentArray));
    //    }

    //    #endregion Methods

    //}

    internal class ConcatenationExpression : Expression
    {

        #region - - - - - - Fields - - - - - -

        private static MethodInfo s_ConcatMethodInfo;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public ConcatenationExpression(Expression first, Expression second)
        {
            this.First = first ?? throw new ArgumentNullException(nameof(first));
            this.Second = second ?? throw new ArgumentNullException(nameof(second));
        }

        public ConcatenationExpression(IQueryable firstQuery, IQueryable secondQuery) : this(
            Constant(firstQuery ?? throw new ArgumentNullException(nameof(firstQuery))),
            Constant(secondQuery ?? throw new ArgumentNullException(nameof(secondQuery))))
        { }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public override bool CanReduce
            => true;

        private Type ElementType
            => this.First.Type.GenericTypeArguments[0];

        public Expression First { get; }

        public override ExpressionType NodeType
            => (ExpressionType)int.MinValue;

        public Expression Second { get; }

        public override Type Type
            => typeof(IQueryable<>).MakeGenericType(this.ElementType);

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        private static IQueryable<TElement> Concat<TElement, TFirstQuery, TSecondQuery>(TFirstQuery first, TSecondQuery second)
            where TFirstQuery : IQueryable<TElement>
            where TSecondQuery : IQueryable<TElement>
            => first.Concat(second);

        private static MethodInfo GetConcatMethodInfo(Type TElement, Type TFirstQuery, Type TSecondQuery)
            => (s_ConcatMethodInfo ??= typeof(ConcatenationExpression).GetMethod(nameof(Concat), BindingFlags.NonPublic | BindingFlags.Static).GetGenericMethodDefinition())
                .MakeGenericMethod(TElement, TFirstQuery, TSecondQuery);

        public override Expression Reduce()
            => Call(null,
                GetConcatMethodInfo(this.ElementType, this.First.Type, this.Second.Type),
                this.First,
                 this.Second);

        #endregion Methods

    }

    //internal class DuplicateTreeExpression : Expression
    //{

    //    #region - - - - - - Fields - - - - - -

    //    private static MethodInfo s_ConcatMethodInfo;

    //    #endregion Fields

    //    #region - - - - - - Constructors - - - - - -

    //    public DuplicateTreeExpression(Expression first, Expression second)
    //    {
    //        this.First = first ?? throw new ArgumentNullException(nameof(first));
    //        this.Second = second ?? throw new ArgumentNullException(nameof(second));
    //    }

    //    #endregion Constructors

    //    #region - - - - - - Properties - - - - - -

    //    public override bool CanReduce
    //        => true;

    //    private Type ElementType
    //        => this.First.Type.GenericTypeArguments[0];

    //    public Expression First { get; }

    //    public override ExpressionType NodeType
    //        => (ExpressionType)int.MinValue;

    //    public Expression Second { get; }

    //    public override Type Type
    //        => typeof(IQueryable<>).MakeGenericType(this.ElementType);

    //    #endregion Properties

    //    #region - - - - - - Methods - - - - - -

    //    private static IQueryable<TElement> Concat<TElement, TFirstQuery, TSecondQuery>(TFirstQuery first, TSecondQuery second)
    //        where TFirstQuery : IQueryable<TElement>
    //        where TSecondQuery : IQueryable<TElement>
    //        => first.Concat(second);

    //    private static MethodInfo GetConcatMethodInfo(Type TElement, Type TFirstQuery, Type TSecondQuery)
    //        => (s_ConcatMethodInfo ??= typeof(DuplicateTreeExpression).GetMethod(nameof(Concat), BindingFlags.NonPublic | BindingFlags.Static).GetGenericMethodDefinition())
    //            .MakeGenericMethod(TElement, TFirstQuery, TSecondQuery);

    //    public override Expression Reduce()
    //        => Call(null,
    //            GetConcatMethodInfo(this.ElementType, this.First.Type, this.Second.Type),
    //            this.First,
    //             this.Second);

    //    #endregion Methods

    //}

    internal class ReduceExpressionVisitor : ExpressionVisitor
    {

        #region - - - - - - Methods - - - - - -

        [return: NotNullIfNotNull("node")]
        public override Expression Visit(Expression node)
            => (node?.CanReduce ?? false) ? this.Visit(node.ReduceAndCheck()) : base.Visit(node);

        #endregion Methods

    }

}
