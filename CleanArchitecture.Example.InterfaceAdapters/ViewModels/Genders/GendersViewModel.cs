using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Example.Application.Services.Pipeline;
using CleanArchitecture.Example.Application.UseCases.People.GetGenders;
using CleanArchitecture.Example.InterfaceAdapters.Entities;
using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Example.InterfaceAdapters.ViewModels.Genders
{

    public class GendersViewModel : IPresenter<IQueryable<GenderDto>>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;
        private readonly IUseCaseInvoker m_UseCaseInvoker;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GendersViewModel(IMapper mapper, IUseCaseInvoker useCaseInvoker)
        {
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_UseCaseInvoker = useCaseInvoker ?? throw new ArgumentNullException(nameof(useCaseInvoker));
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        // Properties

        public Property<ExistingGenderViewModel> SelectedGender { get; } = new Property<ExistingGenderViewModel>();


        // View Models

        public List<ExistingGenderViewModel> Genders { get; private set; }

        #endregion Properties

        #region - - - - - - IPresenter Implementation - - - - - -

        public Task PresentAsync(IQueryable<GenderDto> response, CancellationToken cancellationToken)
        {
            this.Genders = response.ProjectTo<ExistingGenderViewModel>(this.m_Mapper.ConfigurationProvider).ToList();
            return Task.CompletedTask;
        }

        public Task PresentEntityNotFoundAsync(EntityID entityID, CancellationToken cancellationToken)
            => throw new NotImplementedException();

        public Task PresentValidationFailureAsync(ValidationResult validationResult, CancellationToken cancellationToken)
            => throw new NotImplementedException();

        #endregion IPresenter Implementation

        #region - - - - - - Methods - - - - - -

        public Task InitialiseAsync(CancellationToken cancellationToken)
            => this.m_UseCaseInvoker.InvokeUseCaseAsync(new GetGendersRequest(), this, cancellationToken);

        #endregion Methods

    }

}
