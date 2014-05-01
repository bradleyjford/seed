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
        public void Execute_DeactivatingAnInactiveUser_Succeeds()
        {
            var userId = 1;
            var user = _userRepository.Get(userId);

            user.Deactivate();

            var command = new DeactivateUserCommand(userId);

            var result = _commandHandler.Execute(command);

            Assert.IsTrue(result.Success);
            Assert.IsFalse(user.IsActive);
        }

        [Test]
        public void Execute_DeactivatingAnActiveUser_Succeeds()
        {
            var userId = 1;
            var user = _userRepository.Get(userId);

            var command = new DeactivateUserCommand(userId);

            var result = _commandHandler.Execute(command);

            Assert.IsTrue(result.Success);
            Assert.IsFalse(user.IsActive);
        }
    }
}
