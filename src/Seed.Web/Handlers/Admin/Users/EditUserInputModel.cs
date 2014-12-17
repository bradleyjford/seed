using System;
using FluentValidation;
using FluentValidation.Attributes;

namespace Seed.Web.Handlers.Admin.Users
{
    [Validator(typeof(EditUserInputModelValidator))]
    public class EditUserInputModel
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public byte[] RowVersion { get; set; }
    }

    public class EditUserInputModelValidator : AbstractValidator<EditUserInputModel>
    {
        public EditUserInputModelValidator()
        {
            RuleFor(r => r.FullName).NotEmpty();
            RuleFor(r => r.Email).NotEmpty().EmailAddress().WithName("Email");
            RuleFor(r => r.Notes).Length(0, 1024);

            RuleFor(r => r.RowVersion).NotEmpty();
        }
    }
}