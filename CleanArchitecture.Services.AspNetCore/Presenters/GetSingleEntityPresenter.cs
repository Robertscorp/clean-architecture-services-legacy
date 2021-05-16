using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.AspNetCore.Presenters
{

    public class GetSingleEntityPresenter<TResponse> : ActionPresenter<TResponse>
    {

        #region - - - - - - IPresenter Implementation - - - - - -

        public override Task PresentAsync(TResponse response, CancellationToken cancellationToken)
        {
            this.ActionResult = new OkObjectResult(response);
            this.PresentedSuccessfully = true;
            return Task.CompletedTask;
        }

        #endregion IPresenter Implementation

    }

}
