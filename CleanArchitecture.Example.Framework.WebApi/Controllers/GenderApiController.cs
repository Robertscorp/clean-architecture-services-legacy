using CleanArchitecture.Example.Application.UseCases.People.GetGenders;
using CleanArchitecture.Example.InterfaceAdapters.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Example.Framework.WebApi.Controllers
{

    [Route("Genders")]
    public class GenderApiController : BaseController
    {

        #region - - - - - - Fields - - - - - -

        private readonly GenderController m_GenderController;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GenderApiController(GenderController genderController)
            => this.m_GenderController = genderController ?? throw new ArgumentNullException(nameof(genderController));

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        [HttpGet]
        public Task<IActionResult> GetGendersAsync()
            => this.GetManyAsync<GenderDto>(this.m_GenderController.GetGendersAsync, CancellationToken.None);

        #endregion Methods

    }

}
