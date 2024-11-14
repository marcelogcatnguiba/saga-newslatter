namespace Newslatter.Api.Events
{
    public class Message
    {
        public Guid SubscriberId { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}