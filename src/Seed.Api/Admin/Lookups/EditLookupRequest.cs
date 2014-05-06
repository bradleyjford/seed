using System;
using System.ComponentModel.DataAnnotations;

namespace Seed.Api.Admin.Lookups
{
    public class EditLookupRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
    }
}