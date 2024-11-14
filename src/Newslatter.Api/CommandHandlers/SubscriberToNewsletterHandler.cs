using MassTransit;
using Newslatter.Api.Commands;
using Newslatter.Api.Context;
using Newslatter.Api.Events;

namespace Newslatter.Api.CommandHandlers
{
    public class SubscriberToNewsletterHandler(AppDbContext dbContext) : IConsumer<SubscriberToNewsletter>
    {
        public async Task Consume(ConsumeContext<SubscriberToNewsletter> context)
        {
            var subscriber = dbContext.Subscribers.Add(new() 
            {
                Id = Guid.NewGuid(),
                Email = context.Message.Email,
                SubscribedAtUtc = DateTime.Now
            });

            await dbContext.SaveChangesAsync();

            await context.Publish(new SubscriberCreated()
            {
                SubscriberId = subscriber.Entity.Id,
                Email = context.Message.Email,
            });
        }
    }
}