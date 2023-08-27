﻿using Amazon.Runtime.Internal.Util;
using CQRS.Core.Consumer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Query.Infrastructure.Consumer
{
    public class ConsumerHostedService : IHostedService
    {
        private readonly ILogger<ConsumerHostedService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ConsumerHostedService(ILogger<ConsumerHostedService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async  Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Event Consumer is runniong ");
            using (IServiceScope scoe = _serviceProvider.CreateScope()) {
                var eventConsumer = scoe.ServiceProvider.GetRequiredService<IEventConsumer>();
                var topic = Environment.GetEnvironmentVariable("KAFKA_TOPIC");
                Task.Run(() => eventConsumer.Consumer(topic), cancellationToken) ;
            }
             //return
             await Task.CompletedTask;
            
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Event Consumer is Stopped ");
            //return
            await  Task.CompletedTask;
        }
    }
}
