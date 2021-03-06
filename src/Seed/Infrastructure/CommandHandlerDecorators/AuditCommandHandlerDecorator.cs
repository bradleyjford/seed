﻿using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using Seed.Common.CommandHandling;
using Seed.Common.Domain;
using Seed.Infrastructure.Auditing;
using Seed.Infrastructure.Data;
using Seed.Security;

namespace Seed.Infrastructure.CommandHandlerDecorators
{
    public class AuditCommandHandlerDecorator<TCommand, TResult> : ICommandHandler<TCommand, TResult>
        where TCommand : ICommand<TResult> 
        where TResult : class
    {
        private readonly ICommandHandler<TCommand, TResult> _decorated;
        private readonly ISeedDbContext _dbContext;
        private readonly IUserContext<Guid> _userContext;

        public AuditCommandHandlerDecorator(
            ICommandHandler<TCommand, TResult> decorated,
            ISeedDbContext dbContext,
            IUserContext<Guid> userContext)
        {
            _decorated = decorated;
            _dbContext = dbContext;
            _userContext = userContext;
        }

        public async Task<TResult> Handle(TCommand command)
        {
            var result = await _decorated.Handle(command);

            var entry = AuditEvent.Create(_userContext, command);

            _dbContext.AuditEvents.Add(entry);

            ApplyInlineAuditValues(_userContext);

            return result;
        }

        private void ApplyInlineAuditValues(IUserContext<Guid> userContext)
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries())
            {
                var inlineAudited = entry.Entity as IInlineAudited<Guid>;

                if (inlineAudited == null)
                {
                    continue;
                }

                if (entry.State == EntityState.Added)
                {
                    SetCreated(entry, userContext);
                    SetModified(entry, userContext);
                }
                else if (entry.State == EntityState.Modified)
                {
                    SetModified(entry, userContext);
                }
            }
        }

        private void SetCreated(DbEntityEntry entry, IUserContext<Guid> userContext)
        {
            entry.CurrentValues["CreatedUtcDate"] = ClockProvider.GetUtcNow();
            entry.CurrentValues["CreatedByUserId"] = userContext.UserId;
        }

        private void SetModified(DbEntityEntry entry, IUserContext<Guid> userContext)
        {
            entry.CurrentValues["ModifiedUtcDate"] = ClockProvider.GetUtcNow();
            entry.CurrentValues["ModifiedByUserId"] = userContext.UserId;
        }
    }
}
