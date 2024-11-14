using MassTransit;

namespace Newslatter.Api.Saga
{
    public class NewslatterOnboardingSagaData : SagaStateMachineInstance
    {
        public Guid CorrelationId { get ; set ; }
        public string CurrentState { get; set; } = string.Empty;

        public Guid SubscriberId { get; set; }
        public string Email { get; set; } = string.Empty;
        public bool WelcomeEmailSent { get; set; }
        public bool FollowUpEmailSent { get; set; }
        public bool OnboardingCompleted { get; set; }
    }
}