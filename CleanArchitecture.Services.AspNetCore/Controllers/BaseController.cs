using AutoMapper;
using CleanArchitecture.Services.AspNetCore.Presenters;
using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Extended.Pipeline;
using CleanArchitecture.Services.Persistence;
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

        private IPersistenceContext PersistenceContext => (IPersistenceContext)this.HttpContext.RequestServices.GetService(typeof(IPersistenceContext));

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        protected internal async Task<IActionResult> CreateAsync<TRequest, TResponse>(
            TRequest request,
            Func<TRequest, IPresenter<TResponse>, CancellationToken, Task> controllerAction,
            Func<TResponse, string> entityLocationFunc,
            CancellationToken cancellationToken)
        {
            var _Presenter = new CreateEntityPresenter<TResponse>(entityLocationFunc) { Mapper = this.Mapper };

            await controllerAction(request, _Presenter, cancellationToken);

            if (_Presenter.PresentedSuccessfully)
                _ = await this.PersistenceContext.SaveChangesAsync(cancellationToken);

            return _Presenter.ActionResult;
        }

        protected internal async Task<IActionResult> DeleteAsync<TRequest, TResponse>(TRequest request, Func<TRequest, IPresenter<TResponse>, CancellationToken, Task> controllerAction, CancellationToken cancellationToken)
        {
            var _Presenter = new DeleteEntityPresenter<TResponse>() { Mapper = this.Mapper };

            await controllerAction(request, _Presenter, cancellationToken);

            if (_Presenter.PresentedSuccessfully)
                _ = await this.PersistenceContext.SaveChangesAsync(cancellationToken);

            return _Presenter.ActionResult;
        }

        protected internal async Task<IActionResult> GetManyAsync<TResponse>(Func<IPresenter<IQueryable<TResponse>>, CancellationToken, Task> controllerAction, CancellationToken cancellationToken)
        {
            var _Presenter = new GetManyEntitiesPresenter<TResponse>() { Mapper = this.Mapper };

            await controllerAction(_Presenter, cancellationToken);

            return _Presenter.ActionResult;
        }

        protected internal async Task<IActionResult> GetSingleAsync<TResponse>(EntityID entityID, Func<EntityID, IPresenter<TResponse>, CancellationToken, Task> controllerAction, CancellationToken cancellationToken)
        {
            var _Presenter = new GetSingleEntityPresenter<TResponse>() { Mapper = this.Mapper };

            await controllerAction(entityID, _Presenter, cancellationToken);

            return _Presenter.ActionResult;
        }

        protected internal async Task<IActionResult> UpdateAsync<TRequest, TResponse>(TRequest request, Func<TRequest, IPresenter<TResponse>, CancellationToken, Task> controllerAction, CancellationToken cancellationToken)
        {
            var _Presenter = new UpdateEntityPresenter<TResponse>() { Mapper = this.Mapper };

            await controllerAction(request, _Presenter, cancellationToken);

            if (_Presenter.PresentedSuccessfully)
                _ = await this.PersistenceContext.SaveChangesAsync(cancellationToken);

            return _Presenter.ActionResult;
        }

        #endregion Methods

    }

}
