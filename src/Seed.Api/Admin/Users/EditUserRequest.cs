using System;
using FluentValidation;
using FluentValidation.Attributes;

namespace Seed.Api.Admin.Users
{
    [Validator(typeof(EditUserRequestValidator))]
    public class EditUserRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public byte[] RowVersion { get; set; }
    }

    public class EditUserRequestValidator : AbstractValidator<EditUserRequest>
    {
        public EditUserRequestValidator()
        {
            RuleFor(r => r.FullName)
                .NotEmpty()
                .Length(1, 100);

            RuleFor(r => r.Email)
                .NotEmpty()
                .EmailAddress()
                .Length(3, 200);

            RuleFor(r => r.RowVersion)
                .NotEmpty();
        }
    }
}
