using System;

namespace Seed.Api.Admin.Users
{
    public class UserSummaryResponse
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
    }
}