using System;
using NUnit.Framework;
using Seed.Security;

namespace Seed.Tests.Security
{
    [TestFixture]
    public class ActivateUserCommandTests
    {
        private ActivateUserCommandHandler _activateCommandHandler;
        private TestUserRepository _userRepository;

        [SetUp]
        public void SetUp()
        {
            _userRepository = new TestUserRepository();

            _activateCommandHandler = new ActivateUserCommandHandler(_userRepository);
        }

        [Test]
        public void Activate_ActivatingAnActiveUser_Succeeds()
        {
            var userId = 1;

            var command = new ActivateUserCommand(userId);

            var result = _activateCommandHandler.Execute(command);

            var user = _userRepository.Get(userId);

            Assert.IsTrue(result.Success);
            Assert.IsTrue(user.IsActive);
        }

        [Test]
        public void Activate_ActivatingAnInactiveUser_Succeeds()
        {
            var userId = 1;
            var user = _userRepository.Get(userId);

            user.Deactivate();

            var command = new ActivateUserCommand(userId);

            var result = _activateCommandHandler.Execute(command);

            Assert.IsTrue(result.Success);
            Assert.IsTrue(user.IsActive);
        }
    }
}
