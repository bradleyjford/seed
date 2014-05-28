using System;
using System.ComponentModel.DataAnnotations;
using Seed.Common.Domain;

namespace Seed.Security
{
    public class LoginProvider : Entity<int>
    {
        public LoginProvider(string name, string userKey)
        {
            Name = name;
            UserKey = userKey;

            CreatedUtcDate = ClockProvider.GetUtcNow();
        }

        [StringLength(200)]
        [Required]
        public string Name { get; private set; }

        [StringLength(200)]
        [Required]
        public string UserKey { get; private set; }

        public DateTime CreatedUtcDate { get; private set; }
    }
}
