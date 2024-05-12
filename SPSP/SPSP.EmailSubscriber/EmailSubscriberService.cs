using EasyNetQ;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using Microsoft.Extensions.Options;
using SPSP.EmailSubscriber.Utils;
using RabbitMQ.Client;
using System.Reflection;
using System.Data.Common;
using System.Threading.Channels;

namespace SPSP.EmailSubscriber
{
    public class EmailSubscriberService : BackgroundService
    {

        private readonly IBus bus;
        private readonly ILogger<EmailSubscriberService> logger;
        private readonly GmailSMTP gmailSMTPSettings;
        private IConnection _connection;
        private IModel _channel;

        private readonly string _host = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";
        private readonly string _username = Environment.GetEnvironmentVariable("RABBITMQ_USERNAME") ?? "guest";
        private readonly string _password = Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ?? "guest";
        private readonly string _virtualhost = Environment.GetEnvironmentVariable("RABBITMQ_VIRTUALHOST") ?? "/";
        //private readonly string _host = "localhost";
        //private readonly string _username = "guest";
        //private readonly string _password = "guest";
        //private readonly string _virtualhost = "/";
        public EmailSubscriberService(ILogger<EmailSubscriberService> logger, IOptions<GmailSMTP> gmailSMTPSettings, IConfiguration configuration)
        {
            this.logger = logger;
            this.gmailSMTPSettings = gmailSMTPSettings.Value;

            //var rabbitMQHost = configuration["RABBITMQ_HOST"] ?? "localhost";
            //var rabbitMQUsername = configuration["RABBITMQ_USERNAME"] ?? "guest";
            //var rabbitMQPassword = configuration["RABBITMQ_PASSWORD"] ?? "guest";
            //var rabbitMQVirtualHost = configuration["RABBITMQ_VIRTUALHOST"] ?? "/";

            //var rabbitMQConnectionString = $"host={rabbitMQHost};username={rabbitMQUsername};password={rabbitMQPassword};virtualHost={rabbitMQVirtualHost}";

            //this.bus = RabbitHutch.CreateBus(rabbitMQConnectionString);
            //InitRabbitMQ();

        }


        private void InitRabbitMQ()
        {
            var factory = new ConnectionFactory
            {
                HostName = _host,
                UserName = _username,
                Password = _password
            };

            // create connection  
            _connection = factory.CreateConnection();

            // create channel  
            _channel = _connection.CreateModel();

            //_channel.ExchangeDeclare("demo.exchange", ExchangeType.Topic);
            _channel.QueueDeclare("Reservation_added", false, false, false, null);
            //_channel.QueueBind("demo.queue.log", "demo.exchange", "demo.queue.*", null);
            _channel.BasicQos(0, 1, false);

            _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            //await bus.PubSub.SubscribeAsync<Models.EmailMessage>("email-queue", emailMessage =>
            //{
            //     SendErrorMailAsync(emailMessage);
            //});
            Console.WriteLine($"PRIJE WHILE");

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"POSLIJE WHILE");

                try
                {
                    Console.WriteLine($"TRY");

                    Console.WriteLine($"_host: {_host}");
                    Console.WriteLine($"-------------------------------------");
                    Console.WriteLine($"_virtualhost: {_virtualhost}");
                    Console.WriteLine($"-------------------------------------");
                    Console.WriteLine($"username: {_username}");
                    Console.WriteLine($"-------------------------------------");
                    Console.WriteLine($"_password: {_password}");
                    Console.WriteLine($"-------------------------------------");

                    using (var bus = RabbitHutch.CreateBus($"host={_host};virtualHost={_virtualhost};username={_username};password={_password}"))
                    {
                        bus.PubSub.Subscribe<Models.EmailMessage>("email-queue", emailMessage =>
                        {
                            SendErrorMailAsync(emailMessage);
                        });
                        Console.WriteLine("Listening for email messages.");
                        await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
                    }


                    //await this.bus.PubSub.SubscribeAsync<Models.EmailMessage>("email-queue", emailMessage =>
                    //{
                    //    SendErrorMailAsync(emailMessage);
                    //});

                }
                catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
                {
                    // Gracefully handle cancellation
                    break;
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                    Console.WriteLine($"Error in RabbitMQ listener: {ex.Message}");
                    Console.WriteLine($"-------------------------------------");

                    // Log the full exception object
                    Console.WriteLine($"Error object: {ex}");
                    Console.WriteLine($"----------------------------");

                    // Log the stack trace
                    Console.WriteLine($"Stack trace: {ex.StackTrace}");
                    Console.WriteLine($"----------------------------");

                    if (bus == null)
                    {
                        Console.WriteLine("Bus is null.");
                    }
                    else
                    {
                        // Example: Check if the underlying connection is closed
                        var advancedBus = ((RabbitAdvancedBus)bus);
                        if (!advancedBus.IsConnected)
                        {
                            Console.WriteLine("Bus connection is closed.");
                        }
                    }
                }
            }

        }
        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e) { }
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

        //public override void Dispose()
        //{
        //    bus.Dispose();
        //    base.Dispose();
        //}

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            //bus.Dispose();
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
