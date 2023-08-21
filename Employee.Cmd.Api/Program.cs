using CQRS.Core.Domain;
using CQRS.Core.Handler;
using CQRS.Core.Infrastuctur;
using Employee.Cmd.Api.Commands;
using Employee.Cmd.Domain.Aggregate;
using Employee.Cmd.Infrastructure.Config;
using Employee.Cmd.Infrastructure.Hanlder;
using Employee.Cmd.Infrastructure.Repository;
using Employee.Cmd.Infrastructure.Stores;
using System.Diagnostics.Tracing;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHadnler<EmployeeAggregate>, EventSourcingHandler>();
builder.Services.AddScoped<ICommandHandler, CommandHandler>();
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
