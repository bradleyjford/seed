using System;
using Seed.Security;

namespace Seed.Web.Handlers.Admin.Users
{
    public class EditUserViewModel : EditUserInputModel
    {
        public EditUserViewModel(User user, EditUserInputModel inputModel)
        {
            
        }

        public Guid Id { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
    }
}