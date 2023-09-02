using Confluent.Kafka;
using CQRS.Core.Domain;
using CQRS.Core.Events;
using CQRS.Core.Handler;
using CQRS.Core.Infrastuctur;
using CQRS.Core.Producer;
using Employee.Cmd.Api.Commands;
using Employee.Cmd.Domain.Aggregate;
using Employee.Cmd.Infrastructure.Config;
using Employee.Cmd.Infrastructure.Dispatcher;
using Employee.Cmd.Infrastructure.Hanlder;
using Employee.Cmd.Infrastructure.Producer;
using Employee.Cmd.Infrastructure.Repository;
using Employee.Cmd.Infrastructure.Stores;
using Employee.Common.Event;
using Microsoft.AspNetCore.DataProtection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Diagnostics.Tracing;

var builder = WebApplication.CreateBuilder(args);

BsonClassMap.RegisterClassMap<BaseEvent>();
BsonClassMap.RegisterClassMap<EmployeeCreatedEvent>();
BsonClassMap.RegisterClassMap<UpdateEmployeeEvent>();
BsonClassMap.RegisterClassMap<DeleteEmployeeEvent>();
BsonClassMap.RegisterClassMap<DayWorkEvent>();
BsonClassMap.RegisterClassMap<AddVacationEvent>();
// Add services to the container.
builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));

builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
//make sure added it aboce IEventStore .
builder.Services.AddScoped<IEventProducer, EventProducer>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHadnler<EmployeeAggregate>, EventSourcingHandler>();
builder.Services.AddScoped<ICommandHandler, CommandHandler>();

#region cOMMAND hANDLER mETHODS 
var commandHandler = builder.Services.BuildServiceProvider().GetRequiredService<ICommandHandler>();
var dispatcher = new CommandDispatcher();
dispatcher.Register<NewEmployeeCommands>(commandHandler.HandleAsync);
dispatcher.Register<AddVacationCommand>(commandHandler.HandleAsync);
dispatcher.Register<DaysWorkCommand>(commandHandler.HandleAsync);
dispatcher.Register<DeleteEmployeeCommand>(commandHandler.HandleAsync);
dispatcher.Register<EditDepartmentCommand>(commandHandler.HandleAsync);
builder.Services.AddSingleton<ICommandDispatcher>(_=>dispatcher);
#endregion
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
