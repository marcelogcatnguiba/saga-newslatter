using MassTransit;
using Newslatter.Api.Events;

namespace Newslatter.Api.CommandHandlers
{
    public class OnboardingCompletedHandler(ILogger<OnboardingCompletedHandler> logger) : IConsumer<OnboardingCompleted>
    {
        public Task Consume(ConsumeContext<OnboardingCompleted> context)
        {
            logger.LogInformation("Completed ...");

            return Task.CompletedTask;
        }
    }
}