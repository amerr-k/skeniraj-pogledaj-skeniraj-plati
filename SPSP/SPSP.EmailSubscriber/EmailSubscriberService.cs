using EasyNetQ;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using Microsoft.Extensions.Options;
using SPSP.EmailSubscriber.Utils;
using RabbitMQ.Client;

namespace SPSP.EmailSubscriber
{
    public class EmailSubscriberService : BackgroundService
    {
        private readonly ILogger<EmailSubscriberService> logger;
        private readonly GmailSMTP gmailSMTPSettings;

        private readonly string _host = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";
        private readonly string _username = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? "guest";
        private readonly string _password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest";
        private readonly string _virtualhost = Environment.GetEnvironmentVariable("RABBITMQ_VIRTUALHOST") ?? "/";

        public EmailSubscriberService(ILogger<EmailSubscriberService> logger, IOptions<GmailSMTP> gmailSMTPSettings, IConfiguration configuration)
        {
            this.logger = logger;
            this.gmailSMTPSettings = gmailSMTPSettings.Value;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var bus = RabbitHutch.CreateBus($"host={_host};virtualHost={_virtualhost};username={_username};password={_password}"))
                    {
                        bus.PubSub.Subscribe<Models.EmailMessage>("email-queue", emailMessage =>
                        {
                            SendErrorMailAsync(emailMessage);
                        });
                        Console.WriteLine("Listening for email messages.");
                        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                    }
                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }
        public Task SendErrorMailAsync(Models.EmailMessage emailMessage)
        {

            string from = gmailSMTPSettings.From;
            string password = gmailSMTPSettings.Password;
            string host = gmailSMTPSettings.Host;
            string to = gmailSMTPSettings.To;
            string subject = emailMessage.Subject;
            string body = emailMessage.Body;

            var client = new SmtpClient(host)
            {
                Port = 587,
                Credentials = new NetworkCredential(from, password),
                EnableSsl = true
            };

            var message = new MailMessage(from, to, subject, body);

            if (emailMessage.PdfAttachment != null)
            {
                var pdfBytes = emailMessage.PdfAttachment;

                var pdfMemoryStream = new MemoryStream(pdfBytes);
                var attachment = new Attachment(pdfMemoryStream, "invoice.pdf", MediaTypeNames.Application.Pdf);

                message.Attachments.Add(attachment);
            }

            try
            {
                client.Send(message);
                logger.LogInformation($"Email sent");

            }
            catch (Exception ex)
            {
                logger.LogInformation("Failed to send email: " + ex.Message);
            }

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            base.Dispose();
        }

    }
}
