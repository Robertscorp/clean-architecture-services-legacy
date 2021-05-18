using AutoMapper;
using CleanArchitecture.Example.InterfaceAdapters.Entities;
using CleanArchitecture.Example.InterfaceAdapters.ViewModels.Genders;
using CleanArchitecture.Services.Pipeline;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Example.InterfaceAdapters.ViewModels.Customers
{

    public class NewCustomerViewModel // : IPresenter<CustomerDto>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;
        //private readonly IUseCaseInvoker m_UseCaseInvoker;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public NewCustomerViewModel(IMapper mapper, IUseCaseInvoker useCaseInvoker)
        {
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            //this.m_UseCaseInvoker = useCaseInvoker ?? throw new ArgumentNullException(nameof(useCaseInvoker));

            this.Genders = new GendersViewModel(mapper, useCaseInvoker);
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        //// Callbacks

        //public Action<ExistingCustomerViewModel> OnCustomerCreated { get; set; }

        //public Action<ValidationResult> OnValidationFailure { get; set; }


        // Properties

        public Property<string> EmailAddress { get; } = new Property<string>();

        public Property<string> FirstName { get; } = new Property<string>();

        public Property<string> LastName { get; } = new Property<string>();

        public Property<string> MobileNumber { get; } = new Property<string>();


        // ViewModels

        public GendersViewModel Genders { get; }

        #endregion Properties

        //#region - - - - - - IPresenter Implementation - - - - - -

        //public Task PresentAsync(CustomerDto response, CancellationToken cancellationToken)
        //{
        //    this.OnCustomerCreated?.Invoke(this.m_Mapper.Map<ExistingCustomerViewModel>(response));
        //    return Task.CompletedTask;
        //}

        //public Task PresentEntityNotFoundAsync(EntityID entityID, CancellationToken cancellationToken)
        //    => throw new NotImplementedException();

        //public Task PresentValidationFailureAsync(ValidationResult validationResult, CancellationToken cancellationToken)
        //{
        //    this.OnValidationFailure?.Invoke(validationResult);
        //    return Task.CompletedTask;
        //}

        //#endregion IPresenter Implementation

        #region - - - - - - Methods - - - - - -

        public Task CreateCustomerAsync(CancellationToken cancellationToken)
            => null;// this.m_UseCaseInvoker.InvokeUseCaseAsync(this.m_Mapper.Map<CreateCustomerRequest>(this), this, cancellationToken);

        public Task InitialiseAsync(CancellationToken cancellationToken)
            => this.Genders.InitialiseAsync(cancellationToken);

        #endregion Methods

    }

}
