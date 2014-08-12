using System;
using System.IO;
using System.Net.Mail;
using System.Reflection;

namespace Track.Server.Mail
{
	public static class MailMessageSerializer
	{
		private static readonly ConstructorInfo MailWriterConstructor;
		private static readonly MethodInfo SendMethod;
		private static readonly MethodInfo CloseMethod;

		static MailMessageSerializer()
		{
			var mailWriterType = typeof(MailMessage).Assembly.GetType("System.Net.Mail.MailWriter");
			
			MailWriterConstructor = mailWriterType.GetConstructor(
				BindingFlags.Instance | BindingFlags.NonPublic,
				null,
				new [] { typeof(Stream) },
				null);

			CloseMethod = mailWriterType.GetMethod(
				"Close",
				BindingFlags.Instance | BindingFlags.NonPublic);

			SendMethod = typeof(MailMessage).GetMethod(
				"Send",
				BindingFlags.Instance | BindingFlags.NonPublic);
		}

		public static string Serialize(MailMessage message)
		{
			string result;

			using (var stream = new MemoryStream())
			{
				var mailWriter = MailWriterConstructor.Invoke(new object[] { stream });

				SendMethod.Invoke(
					message,
					BindingFlags.Instance | BindingFlags.NonPublic,
					null,
					new [] { mailWriter, true, true },
					null);

				stream.Seek(0, SeekOrigin.Begin);

				using (var reader = new StreamReader(stream))
				{
					result = reader.ReadToEnd();
				}
			}

			return result;
		}
	}
}
