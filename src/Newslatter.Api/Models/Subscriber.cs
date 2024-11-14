namespace Newslatter.Api.Models
{
    public class Subscriber
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public DateTime SubscribedAtUtc { get; set; }
    }
}