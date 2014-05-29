using System;
using System.ComponentModel.DataAnnotations;

namespace Seed.Api.Admin.Lookups
{
    public class CreateLookupRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}