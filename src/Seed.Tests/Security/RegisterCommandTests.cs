using System;
using System.Linq;
using System.Threading.Tasks;
using Seed.Common.CommandHandling;
using Seed.Security;
using Seed.Tests.Admin.Users;
using Seed.Tests.Data;
using Xunit;

namespace Seed.Tests.Security
{
    public class RegisterCommandTests : UserCommandTests
    {
        private readonly RegisterUserCommandHandler _commandHandler;
        private readonly RegisterUserCommandValidator _commandValidator;

        public RegisterCommandTests()
        {
            var dbContext = new TestSeedDbContext();
            var passwordHasher = new TestPasswordHasher();
            var randomNumberGenerator = new TestRandomNumberGenerator();
            var authorizationTokenFactory = new AuthorizationTokenFactory(randomNumberGenerator, passwordHasher);
            var testSmtpContext = new TestSmtpContext();

            _commandHandler = new RegisterUserCommandHandler(
                dbContext, 
                passwordHasher, 
                authorizationTokenFactory,
                testSmtpContext);

            _commandValidator = new RegisterUserCommandValidator(dbContext);
        }

        [Fact]
        public async Task Handle_ValidCommand_CreatesAuthorizationToken()
        {
            var command = CreateCommand();

            var result = await _commandHandler.Handle(command);

            Console.WriteLine(result.TokenSecret);

            Assert.Equal("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA====", result.TokenSecret);
        }

        private static RegisterUserCommand CreateCommand()
        {
            var command = new RegisterUserCommand
            {
                Password = "Pa5Sw0rd123",
                UserName = "user1",
                Email = "user1@bjf.io",
                FullName = "User 1"
            };

            return command;
        }

        [Fact]
        public async Task Validate_DuplicateUserName_ReturnsValidationError()
        {
            AddUser(Guid.NewGuid(), "user1", "User 1 Senior", "email@here.com", "password", true);

            var command = CreateCommand();

            var result = await _commandValidator.Validate(command);

            Assert.False(result.Success());
            Assert.Equal(1, result.Count());
        }

        [Fact]
        public async Task Validate_InvalidPassword_ReturnsValidationError()
        {
            var command = CreateCommand();

            command.Password = "123";

            var result = await _commandValidator.Validate(command);

            Assert.False(result.Success());
            Assert.Equal(1, result.Count());
        }
    }
}
