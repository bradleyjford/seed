using System;
using NUnit.Framework;
using Seed.Security;

namespace Seed.Tests.Security
{
    [TestFixture]
    public class SignInCommandTests
    {
        private SignInCommandHandler _commandHandler    ;

        [SetUp]
        public void SetUp()
        {
            _commandHandler = new SignInCommandHandler();
        }

        [Test]
        public void Execute_CorrectCredentials_ReturnsSuccess()
        {
            var command = new SignInCommand("test", "no-important");

            var result = _commandHandler.Execute(command);

            Assert.IsTrue(result.Success);
        }

        [Test]
        public void Execute_IncorrectCredentials_ReturnsFailure()
        {
            var command = new SignInCommand("incorrect", "no-important");

            var result = _commandHandler.Execute(command);

            Assert.IsFalse(result.Success);
        }
    }
}
