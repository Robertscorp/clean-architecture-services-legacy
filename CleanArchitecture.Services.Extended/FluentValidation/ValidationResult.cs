using CleanArchitecture.Services.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

using FluentValidationFailure = FluentValidation.Results.ValidationFailure;
using FluentValidationResult = FluentValidation.Results.ValidationResult;

namespace CleanArchitecture.Services.Extended.FluentValidation
{

    public class ValidationResult : FluentValidationResult, IValidationResult
    {

        #region - - - - - - Constructors - - - - - -

        private ValidationResult() : base() { }

        private ValidationResult(IEnumerable<FluentValidationFailure> failures) : base(failures) { }

        public ValidationResult(FluentValidationResult validationResult) : base(validationResult.Errors)
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
            => Failure(new[] { new FluentValidationFailure(null, failure) });

        public static ValidationResult Failure(IEnumerable<FluentValidationFailure> failures)
            => new ValidationResult(failures);

        private static void SetRuleSetsExecuted(ValidationResult validationResult, string[] value)
        {
            if (s_SetRuleSetsExecutedAction == null)
            {
                var _ValidationResultParameterExpression = Expression.Parameter(typeof(FluentValidationResult));
                var _ValueParameterExpression = Expression.Parameter(typeof(string[]));
                var _PropertyExpression = Expression.Property(_ValidationResultParameterExpression, nameof(RuleSetsExecuted));
                var _PropertyAssignExpression = Expression.Assign(_PropertyExpression, _ValueParameterExpression);
                var _LambdaExpression = Expression.Lambda<Action<FluentValidationResult, string[]>>(_PropertyAssignExpression, _ValidationResultParameterExpression, _ValueParameterExpression);

                s_SetRuleSetsExecutedAction = _LambdaExpression.Compile();
            }

            s_SetRuleSetsExecutedAction.Invoke(validationResult, value);
        }
        private static Action<FluentValidationResult, string[]> s_SetRuleSetsExecutedAction;

        public static ValidationResult Success()
            => new ValidationResult();

        #endregion Methods

    }

}
