using EasyNetQ;

namespace SPSP.Services.OrderEmailPublisher
{
    public class EmailPublisherService : IEmailPublisherService
    {
        private readonly IBus bus;

        public EmailPublisherService()
        {
            bus = RabbitHutch.CreateBus("host=localhost");
        }
        public void PublishSaleInvoiceEmail(Models.EmailMessage emailMessage)
        {
            bus.PubSub.Publish(emailMessage, "email-queue");

        }
    }
}
