using MassTransit;
using Newslatter.Api.Commands;
using Newslatter.Api.Events;

namespace Newslatter.Api.Saga
{
    public class NewslatterOnboardingSaga : MassTransitStateMachine<NewslatterOnboardingSagaData>
    {
        public State? Welcoming { get; set; }
        public State? FollowingUp { get; set; }
        public State? Onboarding { get; set; }

        public Event<SubscriberCreated>? SubscriberCreated { get; set; }
        public Event<WelcomeEmailSent>? WelcomeEmailSent { get; set; }
        public Event<FollowUpEmailSent>? FollowUpEmailSent { get; set; }

        public NewslatterOnboardingSaga()
        {
            //Transforma os States a cima em string para salvar no bus ou na fila
            InstanceState(x => x.CurrentState);

            Event(() => SubscriberCreated, e => e.CorrelateById(m => m.Message.SubscriberId));
            Event(() => WelcomeEmailSent, e => e.CorrelateById(m => m.Message.SubscriberId));
            Event(() => FollowUpEmailSent, e => e.CorrelateById(m => m.Message.SubscriberId));

            //Fez a inscricao
            Initially(
                When(SubscriberCreated)
                    .Then(context => 
                    {
                        //Este obj Saga é meu NewslatterOnboardingSagaData
                        context.Saga.SubscriberId = context.Message.SubscriberId;
                        context.Saga.Email = context.Message.Email;
                    })
                    .TransitionTo(Welcoming)
                    .Publish(context => new SendWelcomeEmail(context.Message.SubscriberId, context.Message.Email)));

            //Inscricao foi completatada, agora ele muda para seguindo
            During(Welcoming,
                When(WelcomeEmailSent)
                    .Then(context => context.Saga.WelcomeEmailSent = true)
                    .TransitionTo(FollowingUp)
                    .Publish(context => new SendFollowUpEmail(context.Message.SubscriberId, context.Message.Email)));

            During(FollowingUp,
                When(FollowUpEmailSent)
                    .Then(context => 
                    {
                        context.Saga.FollowUpEmailSent = true;
                        context.Saga.OnboardingCompleted = true;
                    })
                    .TransitionTo(Onboarding)
                    .Publish(context => new OnboardingCompleted()
                    {
                        SubscriberId = context.Saga.SubscriberId,
                        Email = context.Saga.Email
                    })
                    .Finalize());
        }
    }
}