using EventStore.ClientAPI;

namespace implementationCQRS.Infrastructure.EventSource
{
    public interface IEventStoreDbContext
    {
        Task<IEventStoreConnection> GetConnection();

        Task AppendToStreamAsync(params EventData[] events);
    }
}
