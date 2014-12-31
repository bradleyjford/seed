using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Seed.Common.CommandHandling
{
    public static class ValidationExtensionMethods
    {
        public static bool Success(this IEnumerable<ValidationResult> results)
        {
            return !results.Any();
        }
    }
}
