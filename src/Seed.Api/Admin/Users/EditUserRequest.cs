using System;
using System.ComponentModel.DataAnnotations;

namespace Seed.Api.Admin.Users
{
    public class EditUserRequest
    {
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(150)]
        public string EmailAddress { get; set; }

        public string Notes { get; set; }

        [Required]
        public byte[] RowVersion { get; set; }
    }
}