using System;
using AutoMapper;
using Seed.Api.Admin.Lookups;
using Seed.Common.Domain;

namespace Seed.Api
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<ILookupEntity, LookupSummaryResponse>();
        }
    }
}
