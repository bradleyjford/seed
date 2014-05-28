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
        public async void Activate_ActivatingAnActiveUser_Succeeds()
        {
            var userId = 1;

            var command = new ActivateUserCommand(userId);

            var result = await _activateCommandHandler.Handle(command);

            var user = await _userRepository.Get(userId);

            Assert.IsTrue(result.Success);
            Assert.IsTrue(user.IsActive);
        }

        [Test]
        public async void Activate_ActivatingAnInactiveUser_Succeeds()
        {
            var userId = 1;
            var user = await _userRepository.Get(userId);

            user.Deactivate();

            var command = new ActivateUserCommand(userId);

            var result = await _activateCommandHandler.Handle(command);

            Assert.IsTrue(result.Success);
            Assert.IsTrue(user.IsActive);
        }
    }
}
