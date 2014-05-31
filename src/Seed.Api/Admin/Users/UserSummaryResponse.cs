using System;

namespace Seed.Api.Admin.Users
{
    public class UserSummaryResponse
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
    }
}