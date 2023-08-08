using EventStore.ClientAPI;
using System.Net;

namespace implementationCQRS.Infrastructure.EventSource
{
    public class EventStoreDbContext : IEventStoreDbContext
    {
        public async Task<IEventStoreConnection> GetConnection()
        {
            var connectionSettings = ConnectionSettings.Create().DisableTls().Build();
            ///EventStore Cluster port 
            IEventStoreConnection connection =
                EventStoreConnection.Create(
                    connectionSettings,
                new IPEndPoint(IPAddress.Loopback, 1113),
                nameof(implementationCQRS));
             connection.ConnectAsync().Wait();

            return connection;
        }

        public async Task AppendToStreamAsync(params EventData[] events)
        {
            const string appName = nameof(implementationCQRS);
            IEventStoreConnection connection = await GetConnection();

            await connection.AppendToStreamAsync(appName, ExpectedVersion.Any, events);
        }
    }
}
