using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.AspNetCore.Presenters
{

    public class UpdateEntityPresenter<TResponse> : ActionPresenter<TResponse>
    {

        #region - - - - - - Methods - - - - - -

        public override Task PresentAsync(TResponse response, CancellationToken cancellationToken)
        {
            this.ActionResult = new OkObjectResult(response);
            this.PresentedSuccessfully = true;
            return Task.CompletedTask;
        }

        #endregion Methods

    }

}
