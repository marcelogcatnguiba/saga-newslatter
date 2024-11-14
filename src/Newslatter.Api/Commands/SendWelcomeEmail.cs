namespace Newslatter.Api.Commands
{
    public record SendWelcomeEmail(Guid SubscriberId, string Email);
}