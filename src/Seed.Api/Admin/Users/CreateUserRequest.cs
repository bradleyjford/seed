using System;
using System.Web.Http;
using FluentValidation;
using FluentValidation.Attributes;

namespace Seed.Api.Admin.Users
{
    [FromBody]
    [Validator(typeof(CreateUserRequestValidator))]
    public class CreateUserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
    {
        public CreateUserRequestValidator()
        {
            RuleFor(r => r.Username)
                .NotEmpty()
                .Length(1, 50);

            RuleFor(r => r.Password)
                .NotEmpty()
                .Length(1, 100);

            RuleFor(r => r.ConfirmPassword)
                .Equal(r => r.Password);

            RuleFor(r => r.FullName)
                .NotEmpty()
                .Length(1, 100);

            RuleFor(r => r.Email)
                .NotEmpty()
                .EmailAddress()
                .Length(3, 200);
        }
    }
}