using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Seed.Common.Net
{
    public interface ISmtpContext
    {
        void Send(MailMessage message);
        Task Commit();
    }

    public class SmtpContext : ISmtpContext
    {
        private readonly List<MailMessage> _messages = new List<MailMessage>();

        private readonly SmtpClient _smtpClient = new SmtpClient();

        public void Send(MailMessage message)
        {
            _messages.Add(message);
        }

        public Task Commit()
        {
            var tasks = new Task[_messages.Count];
            var i = 0;

            foreach (var message in _messages)
            {
                tasks[i++] = _smtpClient.SendMailAsync(message);
            }

            return Task.WhenAll(tasks);
        }
    }
}
