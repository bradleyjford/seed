using System;
using AutoMapper;
using Seed.Admin.Users;
using Seed.Api.Admin.Lookups;
using Seed.Api.Admin.Users;
using Seed.Common.Domain;
using Seed.Security;

namespace Seed.Api
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            // Model -> Response
            Mapper.CreateMap<User, UserSummaryResponse>();
            Mapper.CreateMap<User, UserDetailResponse>();

            Mapper.CreateMap<ILookupEntity, LookupSummaryResponse>();

            // Request -> Commands
            Mapper.CreateMap<EditUserRequest, EditUserCommand>();
        }
    }
}
