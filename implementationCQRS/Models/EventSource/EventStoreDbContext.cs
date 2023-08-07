using EventStore.ClientAPI;
using System.Net;

namespace implementationCQRS.Infrastructure.EventSource
{
    public class EventStoreDbContext : IEventStoreDbContext
    {
        public async Task<IEventStoreConnection> GetConnection()
        {
            ///EventStore Cluster port 
            IEventStoreConnection connection = EventStoreConnection.Create(
                new IPEndPoint(IPAddress.Loopback, 2113),
                nameof(implementationCQRS));

            await connection.ConnectAsync();

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
