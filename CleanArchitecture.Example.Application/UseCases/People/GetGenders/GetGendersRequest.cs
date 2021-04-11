using CleanArchitecture.Services.Pipeline;
using System.Linq;

namespace CleanArchitecture.Example.Application.UseCases.People.GetGenders
{

    public class GetGendersRequest : IUseCaseRequest<IQueryable<GenderDto>>
    {
    }

}
