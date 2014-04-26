using System;
using Newtonsoft.Json;
using Seed.Infrastructure.Domain;
using Seed.Infrastructure.Messaging;

namespace Seed.Common
{
    public class AuditEvent
    {
        public static AuditEvent Create(IUserContext userContext, ICommand command)
        {
            var userId = userContext.UserId;
            var date = ClockProvider.GetUtcNow();

            var commandName = command.GetType().FullName;
            var data = JsonConvert.SerializeObject(command);

            return new AuditEvent(userId, date, commandName, data);
        }

        private AuditEvent(int userId, DateTime date, string command, string data)
        {
            UserId = userId;
            Date = date;
            Command = command;
            Data = data;
        }

        protected AuditEvent()
        {
            
        }

        public int Id { get; protected set; }

        public DateTime Date { get; protected set; }
        public int UserId { get; protected set; }

        public string Command { get; protected set; }
        public string Data { get; protected set; }
    }
}
