using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Seed.Api.Security
{
    public class SignInRequest
    {
        [Required]
        [DisplayName("User name")]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}