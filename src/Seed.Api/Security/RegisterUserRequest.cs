using System;
using FluentValidation;
using FluentValidation.Attributes;
using Seed.Api.Admin.Users;

namespace Seed.Api.Security
{
    [Validator(typeof(EditUserRequestValidator))]
    public class RegisterUserRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string FullName { get; set; }
        public string EmailAddress { get; set; }
    }

    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(r => r.UserName).NotEmpty();
            RuleFor(r => r.Password).NotEmpty();
            RuleFor(r => r.ConfirmPassword).Equal(r => r.Password);
            RuleFor(r => r.FullName).NotEmpty();
            RuleFor(r => r.EmailAddress).EmailAddress().NotEmpty().WithName("Email");
        }
    }
}
