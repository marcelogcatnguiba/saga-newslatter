namespace Newslatter.Api.Commands
{
    public record SendFollowUpEmail(Guid SubscriberId, string Email);
}