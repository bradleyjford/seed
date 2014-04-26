using System;

namespace Seed.Api.Admin.Users
{
    public class GetUserResponse
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
    }
}