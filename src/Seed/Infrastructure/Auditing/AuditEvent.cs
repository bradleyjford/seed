﻿using System;
using Newtonsoft.Json;
using Seed.Common.Auditing.Serialization;
using Seed.Common.CommandHandling;
using Seed.Common.Domain;
using Seed.Security;

namespace Seed.Infrastructure.Auditing
{
    public class AuditEvent : Entity<int>
    {
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            ContractResolver = AuditEntryContractResolver.Instance,
        };

        public static AuditEvent Create(IUserContext<Guid> userContext, ICommand command)
        {
            var userId = userContext.UserId;
            var date = ClockProvider.GetUtcNow();

            var commandName = command.GetType().FullName;
            var data = JsonConvert.SerializeObject(command, JsonSettings);

            return new AuditEvent(userId, date, commandName, data);
        }

        protected AuditEvent()
        {
        }

        private AuditEvent(Guid userId, DateTime date, string command, string data)
        {
            UserId = userId;
            Date = date;
            Command = command;
            Data = data;
        }

        public DateTime Date { get; protected set; }
        public Guid UserId { get; protected set; }

        public string Command { get; protected set; }
        public string Data { get; protected set; }
    }
}
