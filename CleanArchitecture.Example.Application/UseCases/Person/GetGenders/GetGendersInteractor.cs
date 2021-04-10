using AutoMapper;
using CleanArchitecture.Example.Application.Services;
using CleanArchitecture.Example.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CleanArchitecture.Example.Application.UseCases.Person.GetGenders
{

    public class GetGendersInteractor : IUseCaseInteractor<GetGendersRequest, IQueryable<GenderDto>>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public GetGendersInteractor(IMapper mapper)
            => this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        #endregion Constructors

        #region - - - - - - IUseCaseInteractor Implementation - - - - - -

        public Task HandleAsync(GetGendersRequest request, IPresenter<IQueryable<GenderDto>> presenter, CancellationToken cancellationToken)
            => presenter.PresentAsync(this.m_Mapper.Map<List<GenderDto>>(new[]
            {
                GenderEnumeration.Male,
                GenderEnumeration.Female
            }).AsQueryable(), cancellationToken);

        #endregion IUseCaseInteractor Implementation

    }

}
