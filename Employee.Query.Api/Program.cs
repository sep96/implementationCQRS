using Confluent.Kafka;
using CQRS.Core.Consumer;
using Employee.Query.Api.Queries;
using Employee.Query.Domain.Repositories;
using Employee.Query.Infrastructure.Consumer;
using Employee.Query.Infrastructure.DataAccess;
using Employee.Query.Infrastructure.Dispatcher;
using Employee.Query.Infrastructure.Handler;
using Employee.Query.Infrastructure.Repositories;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//
Action<DbContextOptionsBuilder> configureDbContext = o => o.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
builder.Services.AddDbContext<Employee.Query.Infrastructure.DataAccess.ApplicationDbContext>(configureDbContext);
builder.Services.AddSingleton<DataBaseContextFactory>(new DataBaseContextFactory(configureDbContext));
builder.Services.AddScoped<IEventHandler, Employee.Query.Infrastructure.Handler.EventHandler>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IVacationRespository, VacationRespository>();
builder.Services.AddScoped<IQureryHandler, QueryHaNDLER>();
builder.Services.Configure<ConsumerConfig>(builder.Configuration.GetSection(nameof(ConsumerConfig)));
builder.Services.AddScoped<IEventConsumer, EventConsumer>();
builder.Services.AddHostedService<ConsumerHostedService>();
var queryHandler = builder.Services.BuildServiceProvider().GetRequiredService< IQureryHandler>();
var dispatcher = new QueryDispatcher();
dispatcher.RegisterHandler<FindAllEmployeeQuery>(queryHandler.hadnleAsync);
dispatcher.RegisterHandler<FindbyIdEmployee>(queryHandler.hadnleAsync);
//builder.Services.AddScoped()
//create database 
var dbcontext = builder.Services.BuildServiceProvider().GetRequiredService<Employee.Query.Infrastructure.DataAccess.ApplicationDbContext>();
dbcontext.Database.EnsureCreated();
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
