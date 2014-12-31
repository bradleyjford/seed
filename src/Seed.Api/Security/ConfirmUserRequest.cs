using System;
using FluentValidation;
using FluentValidation.Attributes;

namespace Seed.Api.Security
{
    [Validator(typeof(ConfirmUserRequestValidator))]
    public class ConfirmUserRequest
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
    }

    public class ConfirmUserRequestValidator : AbstractValidator<ConfirmUserRequest>
    {
        public ConfirmUserRequestValidator()
        {
            RuleFor(r => r.Id).NotEmpty();
            RuleFor(r => r.Token).NotEmpty();
        }
    }
}