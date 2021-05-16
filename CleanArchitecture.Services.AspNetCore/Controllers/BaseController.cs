using AutoMapper;
using CleanArchitecture.Services.AspNetCore.Presenters;
using CleanArchitecture.Services.Extended.Pipeline;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Services.AspNetCore.Controllers
{

    public abstract class BaseController : ControllerBase
    {

        #region - - - - - - Properties - - - - - -

        public IMapper Mapper => (IMapper)this.HttpContext.RequestServices.GetService(typeof(IMapper));

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public async Task<IActionResult> GetManyAsync<TResponse>(Func<IPresenter<IQueryable<TResponse>>, CancellationToken, Task> controllerAction)
        {
            var _QueryPresenter = new GetManyEntitiesPresenter<TResponse>() { Mapper = this.Mapper };

            await controllerAction(_QueryPresenter, CancellationToken.None);

            return _QueryPresenter.ActionResult;
        }

        #endregion Methods

    }

}
