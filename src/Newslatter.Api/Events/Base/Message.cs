namespace Newslatter.Api.Events.Base
{
    public class Message
    {
        public Guid SubscriberId { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}