using System;
using FluentValidation;
using FluentValidation.Attributes;

namespace Seed.Api.Admin.Users
{
    [Validator(typeof(EditUserRequestValidator))]
    public class EditUserRequest
    {
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
        public string Notes { get; set; }
        public byte[] RowVersion { get; set; }
    }

    public class EditUserRequestValidator : AbstractValidator<EditUserRequest>
    {
        public EditUserRequestValidator()
        {
            RuleFor(r => r.FullName).NotEmpty();
            RuleFor(r => r.EmailAddress).NotEmpty().EmailAddress().WithName("Email");
            RuleFor(r => r.Notes).Length(0, 1024);

            RuleFor(r => r.RowVersion).NotEmpty();
        }
    }
}