using System;

namespace Seed.Web.Handlers.Admin.Users
{
    public class UserSummaryViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsActive { get; set; }
    }
}