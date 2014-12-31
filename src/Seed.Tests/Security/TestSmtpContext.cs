using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using Seed.Common.Net;

namespace Seed.Tests.Security
{
    public class TestSmtpContext : ISmtpContext
    {
        private List<MailMessage> _messages = new List<MailMessage>(); 

        public void Send(MailMessage message)
        {
            _messages.Add(message);
        }

        public IEnumerable<MailMessage> Messages
        {
            get { return _messages; }
        }

        public Task Commit()
        {
            return Task.FromResult(0);
        }
    }
}
