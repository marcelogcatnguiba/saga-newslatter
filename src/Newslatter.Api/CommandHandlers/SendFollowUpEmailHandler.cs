using MassTransit;
using Newslatter.Api.Commands;
using Newslatter.Api.Events;

namespace Newslatter.Api.CommandHandlers
{
    public class SendFollowUpEmailHandler(ILogger<SendFollowUpEmailHandler> logger) : IConsumer<SendFollowUpEmail>
    {
        public async Task Consume(ConsumeContext<SendFollowUpEmail> context)
        {
            logger.LogInformation("Enviando email de follow up para: {}", context.Message.Email);

            await context.Publish(new FollowUpEmailSent()
            {
                SubscriberId = context.Message.SubscriberId,
                Email = context.Message.Email
            });
        }
    }
}