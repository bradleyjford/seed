using System;
using Stateless;

namespace Seed.Security
{
    partial class User
    {
        internal enum UserState
        {
            New,
            Confirmed,
            Activated,
            Deactivated,
            Locked,
            PasswordChangeRequired
        }

        private enum Trigger
        {
            ConfirmEmail,
            Activate,
            Deactivate,
            Lock,
            UnlockAccount,
            RequireChangePassword,
            PasswordChanged
        }

        private class UserStateMachine : StateMachine<UserState, Trigger>
        {
            public UserStateMachine(Func<UserState> stateAccessor, Action<UserState> stateMutator) 
                : base(stateAccessor, stateMutator)
            {
                Configure(UserState.New)
                    .Permit(Trigger.ConfirmEmail, UserState.Confirmed);

                Configure(UserState.Confirmed)
                    .Permit(Trigger.Activate, UserState.Activated);

                Configure(UserState.Activated)
                    .SubstateOf(UserState.Confirmed)
                    .Permit(Trigger.Deactivate, UserState.Deactivated)
                    .Permit(Trigger.Lock, UserState.Locked)
                    .Permit(Trigger.RequireChangePassword, UserState.PasswordChangeRequired);

                Configure(UserState.Deactivated)
                    .SubstateOf(UserState.Confirmed)
                    .Permit(Trigger.Activate, UserState.Activated)
                    .Ignore(Trigger.Deactivate);

                Configure(UserState.Locked)
                    .SubstateOf(UserState.Activated)
                    .SubstateOf(UserState.Deactivated)
                    .Permit(Trigger.UnlockAccount, UserState.Activated)
                    .Ignore(Trigger.PasswordChanged)
                    .Ignore(Trigger.Lock);

                Configure(UserState.PasswordChangeRequired)
                    .SubstateOf(UserState.Confirmed);
            }
        }
    }
}
