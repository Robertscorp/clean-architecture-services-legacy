using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.AspNetCore.Presenters
{

    public class GetManyEntitiesPresenter<TResponse> : ActionPresenter<IQueryable<TResponse>>
    {

        #region - - - - - - Methods - - - - - -

        public override Task PresentAsync(IQueryable<TResponse> response, CancellationToken cancellationToken)
        {
            this.ActionResult = new OkObjectResult(response);
            this.PresentedSuccessfully = true;
            return Task.CompletedTask;
        }

        #endregion Methods

    }

}
