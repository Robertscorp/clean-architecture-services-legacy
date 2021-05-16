using AutoMapper;
using CleanArchitecture.Example.Domain.Entities;
using CleanArchitecture.Services.Entities;
using CleanArchitecture.Services.Extended.AutoMapper;
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
                    .ForMember(dest => dest.ID, opts => opts.Ignore())
                    .ForMember(dest => dest.Role, opts => opts.ConvertUsing<EntityIDConverter<EmployeeRole>, EntityID>(src => src.GenderID))
                    .ForMember(dest => dest.Title, opts => opts.Ignore())
                    .ForMember(dest => dest.User, opts => opts.MapFrom(src => src));

            _ = this.CreateMap<CreateUserRequest, Person>()
                    .ForMember(dest => dest.EmailAddress, opts => opts.Ignore())
                    .ForMember(dest => dest.Gender, opts => opts.ConvertUsing<EntityIDConverter<Gender>, EntityID>(src => src.GenderID))
                    .ForMember(dest => dest.ID, opts => opts.Ignore())
                    .ForMember(dest => dest.MobileNumber, opts => opts.Ignore());

            _ = this.CreateMap<CreateUserRequest, User>()
                    .ForMember(dest => dest.HashedPassword, opts => opts.MapFrom<HashedPasswordValueResolver>())
                    .ForMember(dest => dest.ID, opts => opts.Ignore());
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
