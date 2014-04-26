using System;
using System.ComponentModel.DataAnnotations;

namespace Seed.Api.Admin.Users
{
    public class DeactivateUserRequest
    {
        [Required]
        public int Id { get; set; }
    }
}