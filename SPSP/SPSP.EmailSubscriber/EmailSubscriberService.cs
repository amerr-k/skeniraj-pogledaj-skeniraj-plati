using EasyNetQ;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using Microsoft.Extensions.Options;
using SPSP.EmailSubscriber.Utils;

namespace SPSP.EmailSubscriber
{
    public class EmailSubscriberService : BackgroundService
    {

        private readonly IBus bus;
        private readonly ILogger<EmailSubscriberService> logger;
        private readonly GmailSMTP gmailSMTPSettings;

        public EmailSubscriberService(ILogger<EmailSubscriberService> logger, IOptions<GmailSMTP> gmailSMTPSettings)
        {
            this.logger = logger;
            this.gmailSMTPSettings = gmailSMTPSettings.Value;
            this.bus = RabbitHutch.CreateBus("host=localhost");

        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            await bus.PubSub.SubscribeAsync<Models.EmailMessage>("email-queue", emailMessage =>
            {
                 SendErrorMailAsync(emailMessage);
            });

        }

        public Task SendErrorMailAsync(Models.EmailMessage emailMessage)
        {

            string from = gmailSMTPSettings.From;
            string password = gmailSMTPSettings.Password;
            string host = gmailSMTPSettings.Host;
            string to = "spsp.customer@gmail.com";
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
            bus.Dispose();
            base.Dispose();
        }


        //private async Task SendErrorMailEventHandler(object? model, BasicDeliverEventArgs eventArgs)
        //{
        //    _logger.LogInformation($"Sending email: {model}");
        //    if (Debugger.IsAttached) return;

        //    //var body = eventArgs.Body.ToArray();
        //    //var message = Encoding.UTF8.GetString(body);
        //    await SendErrorMailAsync(model.ToString());
        //}

        //public async Task SendErrorMailAsync(string message)
        //{
        //    try
        //    {
        //        var configuration = new Configuration();
        //        configuration.ApiKey.Add("x-api-key", "f313e5559aea853c33b792fd0b86fa0a3e4dfb8551937cbbbb23d44d664f42b5");

        //        var inboxController = new InboxControllerApi(configuration);

        //        // Upload PDF file and obtain attachment ID
        //        var uploadController = new UploadControllerApi(configuration);
        //        var uploadResult = await uploadController.UploadFileAsync("path_to_your_pdf_file.pdf");
        //        var attachmentId = uploadResult.Id;

        //        var sendEmailOptions = new SendEmailOptions
        //        {
        //            UseInboxName = true,
        //            Subject = "HRMS Error Message",
        //            Body = message,
        //            Attachments = new List<string> { attachmentId },
        //            IsHTML = true,
        //            To = new List<string> { "amer.kovac.pmf@gmail.com" }
        //        };

        //        await inboxController.SendEmailAndConfirmAsync(Guid.Parse("cef5d923-2599-49ec-98c5-af91e36cf614"), sendEmailOptions);
        //        _logger.LogInformation("Email sent to " + new List<string> { "amer.kovac.pmf@gmail.com" } + ".");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogInformation(ex, "Send Error Mail exception");
        //    }
        //}

    }
}
