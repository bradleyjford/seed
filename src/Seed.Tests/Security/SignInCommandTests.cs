﻿using System;
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
            _commandHandler = new SignInCommandHandler(new TestUserRepository(), null);
        }

        [Test]
        public async void Execute_CorrectCredentials_ReturnsSuccess()
        {
            var command = new SignInCommand("test", "no-important");

            var result = await _commandHandler.Handle(command);

            Assert.IsTrue(result.Success);
        }

        [Test]
        public async void Execute_IncorrectCredentials_ReturnsFailure()
        {
            var command = new SignInCommand("incorrect", "no-important");

            var result = await _commandHandler.Handle(command);

            Assert.IsFalse(result.Success);
        }
    }
}
