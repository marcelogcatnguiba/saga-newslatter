using MassTransit;
using Newslatter.Api.Context;
using Newslatter.Api.Saga;

namespace Newslatter.Api.Extensions
{
    public static class ConfigureMassTransit
    {
        public static void AddMassTransitConfigure(this IServiceCollection service, IConfigurationManager configuration)
        {
            service.AddMassTransit(bus =>
            {
                //Seta letras minusculas separadas por hifen "-" => meu-endpoint
                bus.SetKebabCaseEndpointNameFormatter();

                //Registra os consumers do assembly
                bus.AddConsumers(typeof(Program).Assembly);
                
                // bus.UsingInMemory();
                
                bus.UsingRabbitMq((context, config) => 
                {
                    config.Host(configuration.GetConnectionString("RabbitMq"), host =>
                    {
                        host.Username("guest");
                        host.Password("guest");
                    });

                    //Deve salvar no Db cada mudanca de estado da saga
                    config.UseInMemoryOutbox(context);

                    config.ConfigureEndpoints(context);
                });

                //Configura a saga no MassTransit
                bus.AddSagaStateMachine<NewslatterOnboardingSaga, NewslatterOnboardingSagaData>()
                    //Caso for salvar no banco necessario esta parte abaixo.
                    .EntityFrameworkRepository(r => 
                    {
                        r.ExistingDbContext<AppDbContext>();

                        r.UseSqlServer();
                    });
            });
        }
    }
}