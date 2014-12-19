using System;
using System.Threading.Tasks;
using Seed.Common.CommandHandling;
using Seed.Infrastructure.Net;

namespace Seed.Infrastructure.CommandHandlerDecorators
{
    public class SmtpContextCommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult> 
        where TCommand : ICommand<TResult> 
        where TResult : class
    {
        private readonly ICommandHandler<TCommand, TResult> _decorated;
        private readonly ISmtpContext _smtpContext;

        public SmtpContextCommandHandlerDecorator(
            ICommandHandler<TCommand, TResult> decorated,
            ISmtpContext smtpContext)
        {
            _decorated = decorated;
            _smtpContext = smtpContext;
        }

        public Task<TResult> Handle(TCommand command)
        {
            var result = _decorated.Handle(command);

            _smtpContext.Commit();
            
            return result;
        }
    }
}
