using CleanArchitecture.Example.Application.Dtos;
using CleanArchitecture.Example.Application.UseCases.Customers.CreateCustomer;
using CleanArchitecture.Example.Application.UseCases.Customers.DeleteCustomer;
using CleanArchitecture.Example.InterfaceAdapters.Controllers;
using CleanArchitecture.Services.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Example.Framework.WebApi.Controllers
{

    [Route("Customers")]
    public class CustomerApiController : BaseController
    {

        #region - - - - - - Fields - - - - - -

        private readonly CustomerController m_CustomerController;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CustomerApiController(CustomerController customerController)
            => this.m_CustomerController = customerController ?? throw new ArgumentNullException(nameof(customerController));

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        [HttpGet("{customerID}")]
        public Task<IActionResult> GetCustomer([FromRoute] EntityID customerID)
            => this.GetSingleAsync<CustomerDto>(customerID, this.m_CustomerController.GetCustomerAsync, CancellationToken.None);

        [HttpGet]
        public Task<IActionResult> GetCustomers()
            => this.GetManyAsync<CustomerDto>(this.m_CustomerController.GetCustomersAsync, CancellationToken.None);

        [HttpPost]
        public Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
            => this.CreateAsync<CreateCustomerRequest, CustomerDto>(
                request,
                this.m_CustomerController.CreateCustomerAsync,
                customer => this.Url.Action(nameof(GetCustomer), "CustomerApi", new { customerID = customer.CustomerID }),
                CancellationToken.None);

        //xxx // In Create, the CustomerID isn't being serialised properly. It comes through as "/Customers/EntityID"
        //    // In Get, the endpoint returns a 404 for some reason. The EntityID is being deserialised correctly, it just can't be found in the context.

        [HttpDelete("{" + nameof(DeleteCustomerRequest.CustomerID) + "}")]
        public Task<IActionResult> DeleteCustomer([FromRoute] DeleteCustomerRequest request)
            => this.DeleteAsync<DeleteCustomerRequest, CustomerDto>(request, this.m_CustomerController.DeleteCustomerAsync, CancellationToken.None);

        #endregion Methods

    }

}
