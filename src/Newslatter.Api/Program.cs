using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Newslatter.Api.Commands;
using Newslatter.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabaseConfigure(builder.Configuration);
builder.Services.AddMassTransitConfigure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/newsletter", async ([FromBody] string email, IBus bus) => 
{
    //Comando que ira iniciar a saga
    await bus.Publish(new SubscriberToNewsletter(email));
    
    //Endpoint so precisa enviar a mensagem, a partir disso seu trabalho esta feito
    return Results.Accepted();
});

app.UseHttpsRedirection();
app.Run();