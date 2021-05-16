using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.AspNetCore.Presenters
{

    public class CreateEntityPresenter<TResponse> : ActionPresenter<TResponse>
    {

        #region - - - - - - Fields - - - - - -

        private readonly Func<TResponse, string> m_EntityLocationFunc;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreateEntityPresenter(Func<TResponse, string> entityLocationFunc)
            => this.m_EntityLocationFunc = entityLocationFunc ?? throw new ArgumentNullException(nameof(entityLocationFunc));

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public override Task PresentAsync(TResponse response, CancellationToken cancellationToken)
        {
            this.ActionResult = new LateCreatedResult(response, this.m_EntityLocationFunc);
            this.PresentedSuccessfully = true;
            return Task.CompletedTask;
        }

        #endregion Methods

        #region - - - - - - Nested Classes - - - - - -

        /// <summary>
        /// Delegates creating the result until requested. Allows Entities to be persisted before generating the response.
        /// </summary>
        private class LateCreatedResult : CreatedResult
        {

            #region - - - - - - Fields - - - - - -

            private readonly Func<TResponse, string> m_EntityLocationFunc;

            #endregion Fields

            #region - - - - - - Constructors - - - - - -

            public LateCreatedResult(TResponse response, Func<TResponse, string> entityLocationFunc) : base(string.Empty, response)
                => this.m_EntityLocationFunc = entityLocationFunc ?? throw new ArgumentNullException(nameof(entityLocationFunc));

            #endregion Constructors

            #region - - - - - - Methods - - - - - -

            public override Task ExecuteResultAsync(ActionContext context)
            {
                this.Location = this.m_EntityLocationFunc.Invoke((TResponse)this.Value);

                return base.ExecuteResultAsync(context);
            }

            #endregion Methods

        }

        #endregion Nested Classes

    }

}
