using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.AspNetCore.Presenters
{

    public class DeleteEntityPresenter<TResponse> : ActionPresenter<TResponse>
    {

        #region - - - - - - Methods - - - - - -

        public override Task PresentAsync(TResponse response, CancellationToken cancellationToken)
        {
            this.ActionResult = new NoContentResult();
            this.PresentedSuccessfully = true;
            return Task.CompletedTask;
        }

        #endregion Methods

    }

}
