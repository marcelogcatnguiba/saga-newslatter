using MassTransit;
using Newslatter.Api.Commands;
using Newslatter.Api.Events;

namespace Newslatter.Api.CommandHandlers
{
    public class SendWelcomeEmailHandler(ILogger<SendWelcomeEmailHandler> logger) : IConsumer<SendWelcomeEmail>
    {
        public async Task Consume(ConsumeContext<SendWelcomeEmail> context)
        {
            logger.LogInformation("Enviando email de welcome: {}", context.Message.Email);

            await context.Publish(new WelcomeEmailSent()
            {
                SubscriberId = context.Message.SubscriberId,
                Email = context.Message.Email
            });
        }
    }
}