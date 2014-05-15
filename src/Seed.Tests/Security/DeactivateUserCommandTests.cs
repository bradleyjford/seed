using System;
using NUnit.Framework;
using Seed.Security;

namespace Seed.Tests.Security
{
    [TestFixture]
    public class DectivateUserCommandTests
    {
        private DeactivateUserCommandHandler _commandHandler;
        private TestUserRepository _userRepository;

        [SetUp]
        public void SetUp()
        {
            _userRepository = new TestUserRepository();

            _commandHandler = new DeactivateUserCommandHandler(_userRepository);
        }

        [Test]
        public async void Execute_DeactivatingAnInactiveUser_Succeeds()
        {
            var userId = 1;
            var user = await _userRepository.Get(userId);

            user.Deactivate();

            var command = new DeactivateUserCommand(userId);

            var result = await _commandHandler.Handle(command);

            Assert.IsTrue(result.Success);
            Assert.IsFalse(user.IsActive);
        }

        [Test]
        public async void Execute_DeactivatingAnActiveUser_Succeeds()
        {
            var userId = 1;
            var user = await _userRepository.Get(userId);

            var command = new DeactivateUserCommand(userId);

            var result = await _commandHandler.Handle(command);

            Assert.IsTrue(result.Success);
            Assert.IsFalse(user.IsActive);
        }
    }
}
