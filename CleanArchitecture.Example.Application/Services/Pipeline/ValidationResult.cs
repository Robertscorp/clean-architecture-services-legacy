using CleanArchitecture.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CleanArchitecture.Example.Application.Services.Pipeline
{

    public class ValidationResult : FluentValidation.Results.ValidationResult, IValidationResult
    {

        #region - - - - - - Constructors - - - - - -

        private ValidationResult() : base() { }

        private ValidationResult(IEnumerable<FluentValidation.Results.ValidationFailure> failures) : base(failures) { }

        public ValidationResult(FluentValidation.Results.ValidationResult validationResult) : base(validationResult.Errors)
            => this.RuleSetsExecuted = validationResult.RuleSetsExecuted;

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public new string[] RuleSetsExecuted
        {
            get => base.RuleSetsExecuted;
            set => SetRuleSetsExecuted(this, value);
        }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public static ValidationResult Failure(string failure)
            => Failure(new[] { new FluentValidation.Results.ValidationFailure(null, failure) });

        public static ValidationResult Failure(IEnumerable<FluentValidation.Results.ValidationFailure> failures)
            => new ValidationResult(failures);

        private static void SetRuleSetsExecuted(ValidationResult validationResult, string[] value)
        {
            if (s_SetRuleSetsExecutedAction == null)
            {
                var _ValidationResultParameterExpression = Expression.Parameter(typeof(FluentValidation.Results.ValidationResult));
                var _ValueParameterExpression = Expression.Parameter(typeof(string[]));
                var _PropertyExpression = Expression.Property(_ValidationResultParameterExpression, nameof(RuleSetsExecuted));
                var _PropertyAssignExpression = Expression.Assign(_PropertyExpression, _ValueParameterExpression);
                var _LambdaExpression = Expression.Lambda<Action<FluentValidation.Results.ValidationResult, string[]>>(_PropertyAssignExpression, _ValidationResultParameterExpression, _ValueParameterExpression);

                s_SetRuleSetsExecutedAction = _LambdaExpression.Compile();
            }

            s_SetRuleSetsExecutedAction.Invoke(validationResult, value);
        }
        private static Action<FluentValidation.Results.ValidationResult, string[]> s_SetRuleSetsExecutedAction;

        public static ValidationResult Success()
            => new ValidationResult();

        #endregion Methods

    }

}
