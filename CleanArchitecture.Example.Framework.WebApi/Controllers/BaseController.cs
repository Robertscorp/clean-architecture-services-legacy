using CleanArchitecture.Example.Framework.WebApi.Presenters;
using CleanArchitecture.Services.Extended.Pipeline;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Example.Framework.WebApi.Controllers
{

    [ApiController]
    public class BaseController
    {

        #region - - - - - - Methods - - - - - -

        public async Task<IActionResult> GetManyAsync<TResponse>(Func<IPresenter<IQueryable<TResponse>>, CancellationToken, Task> controllerAction)
        {
            var _QueryPresenter = new GetManyEntitiesPresenter<TResponse>();

            await controllerAction(_QueryPresenter, CancellationToken.None);

            return _QueryPresenter.ActionResult;
        }

        #endregion Methods

    }

}
