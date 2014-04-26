using System;
using System.ComponentModel.DataAnnotations;

namespace Seed.Api.Admin.Users
{
    public class SaveUserRequest
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
    }
}