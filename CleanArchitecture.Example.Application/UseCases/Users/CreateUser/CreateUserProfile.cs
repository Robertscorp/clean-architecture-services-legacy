using AutoMapper;
using CleanArchitecture.Example.Application.Extensions;
using CleanArchitecture.Example.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;

namespace CleanArchitecture.Example.Application.UseCases.Users.CreateUser
{

    public class CreateUserProfile : Profile
    {

        #region - - - - - - Constructors - - - - - -

        public CreateUserProfile()
        {
            _ = this.CreateMap<CreateUserRequest, Employee>()
                    .ForMember(dest => dest.EmployeeDetails, opts => opts.MapFrom(src => src))
                    .ForMember(dest => dest.Role, opts => opts.MapFromEntityID(src => src.EmployeeRoleID))
                    .ForMember(dest => dest.Title, opts => opts.Ignore())
                    .ForMember(dest => dest.User, opts => opts.MapFrom(src => src));

            _ = this.CreateMap<CreateUserRequest, Person>()
                    .ForMember(dest => dest.EmailAddress, opts => opts.Ignore())
                    .ForMember(dest => dest.Gender, opts => opts.MapFromEntityID(src => src.GenderID))
                    .ForMember(dest => dest.MobileNumber, opts => opts.Ignore());

            _ = this.CreateMap<CreateUserRequest, User>()
                    .ForMember(dest => dest.HashedPassword, opts => opts.MapFrom<HashedPasswordValueResolver>());

            _ = this.CreateMap<Employee, UserDto>()
                    .ForMember(dest => dest.EmployeeID, opts => opts.MapFromEntity(src => src))
                    .ForMember(dest => dest.EmployeeRoleID, opts => opts.MapFromEnumeration(src => src.Role))
                    .ForMember(dest => dest.FirstName, opts => opts.MapFrom(src => src.EmployeeDetails.FirstName))
                    .ForMember(dest => dest.GenderID, opts => opts.MapFromEnumeration(src => src.EmployeeDetails.Gender))
                    .ForMember(dest => dest.HashedPassword, opts => opts.MapFrom(src => src.User.HashedPassword))
                    .ForMember(dest => dest.LastName, opts => opts.MapFrom(src => src.EmployeeDetails.LastName))
                    .ForMember(dest => dest.PersonID, opts => opts.MapFromEntity(src => src.EmployeeDetails))
                    .ForMember(dest => dest.UserID, opts => opts.MapFromEntity(src => src.User))
                    .ForMember(dest => dest.UserName, opts => opts.MapFrom(src => src.User.UserName));
        }

        #endregion Constructors

    }

    public class HashedPasswordValueResolver : IValueResolver<CreateUserRequest, User, string>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IPasswordHasher<User> m_PasswordHasher;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public HashedPasswordValueResolver(IPasswordHasher<User> passwordHasher)
            => this.m_PasswordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));

        #endregion Constructors

        #region - - - - - - IValueResolver Implementation - - - - - -

        public string Resolve(CreateUserRequest source, User destination, string destMember, ResolutionContext context)
            => this.m_PasswordHasher.HashPassword(destination, source.PlainTextPassword);

        #endregion IValueResolver Implementation

    }

}
