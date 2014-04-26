using System;
using AutoMapper;
using Seed.Admin.Users;
using Seed.Api.Admin.Users;
using Seed.Api.Security;
using Seed.Security;

namespace Seed.Api
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            // Model -> Response
            Mapper.CreateMap<User, GetUserResponse>();

            // Request -> Commands
            Mapper.CreateMap<SignInRequest, SignInCommand>();

            Mapper.CreateMap<SaveUserRequest, EditUserCommand>()
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.Id));

            Mapper.CreateMap<DeactivateUserRequest, DeactivateUserCommand>()
                .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.Id));
        }

    }
}