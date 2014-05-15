using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;

namespace Seed.Infrastructure.Messaging
{
    public class CommandBusBuilder : ICommandBusBuilder
    {
        private readonly List<CommandInterceptorRegistration> _interceptors = new List<CommandInterceptorRegistration>();
        private readonly CommandBusFactory _commandBusFactory;

        public CommandBusBuilder()
        {
            _commandBusFactory = new CommandBusFactory();
        }

        public ICommandBusBuilder Use<TInterceptor>()
            where TInterceptor : ICommandInterceptor
        {
            _interceptors.Add(new CommandInterceptorRegistration(typeof(TInterceptor)));

            return this;
        }

        public ICommandBusBuilder Use<TInterceptor>(params object[] options)
            where TInterceptor : ICommandInterceptor
        {
            _interceptors.Add(new CommandInterceptorRegistration(typeof(TInterceptor), options));

            return this;
        }

        public ICommandBusBuilder Use<TInterceptor>(int index)
            where TInterceptor : ICommandInterceptor
        {
            _interceptors.Insert(index, new CommandInterceptorRegistration(typeof(TInterceptor)));

            return this;
        }

        public ICommandBusBuilder Use<TInterceptor>(int index, params object[] options)
            where TInterceptor : ICommandInterceptor
        {
            _interceptors.Insert(index, new CommandInterceptorRegistration(typeof(TInterceptor), options));

            return this;
        }
        
        public ICommandBus Build(IComponentContext componentContext)
        {
            return _commandBusFactory.Build(componentContext, _interceptors);
        }
    }

    public class CommandBusFactory
    {
        public ICommandBus Build(IComponentContext componentContext, List<CommandInterceptorRegistration> interceptorRegistrations)
        {
            var interceptors = new List<ICommandInterceptor>();

            foreach (var registration in interceptorRegistrations)
            {
                var interceptor = (ICommandInterceptor)componentContext.Resolve(registration.InterceptorType);

                interceptors.Add(interceptor);
            }

            return new CommandBus(componentContext, interceptors);
        }
    }

    public class CommandInterceptorRegistration
    {
        public Type InterceptorType { get; set; }
        public object[] Options { get; set; }

        public CommandInterceptorRegistration(Type interceptorType, params object[] options)
        {
            InterceptorType = interceptorType;
            Options = options;
        }
    }

    public interface ICommandBusBuilder
    {
        ICommandBusBuilder Use<TInterceptor>()
            where TInterceptor : ICommandInterceptor;

        ICommandBusBuilder Use<TInterceptor>(params object[] options)
            where TInterceptor : ICommandInterceptor;

        ICommandBusBuilder Use<TInterceptor>(int index)
            where TInterceptor : ICommandInterceptor;

        ICommandBusBuilder Use<TInterceptor>(int index, params object[] options)
            where TInterceptor : ICommandInterceptor;

        ICommandBus Build(IComponentContext componentContext);
    }

    public interface ICommandInterceptor
    {
        Task PreExecute(ICommandContext context);
        Task PostExecute(ICommandContext context);

        bool ShouldIntercept(ICommand command);
    }

    public interface ICommandContext
    {
        ICommand Command { get; }
        ICommandResult Result { get; set; }
        bool AbortExecution { get; set; }
    }

    public class CommandContext : ICommandContext
    {
        public CommandContext(ICommand command)
        {
            Command = command;
        }

        public ICommand Command { get; private set; }
        public ICommandResult Result { get; set; }
        public bool AbortExecution { get; set; }
    }
}
