using CQRS.Core.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Handler
{
    public interface IEventSourcingHadnler<T>
    {
        Task SaveAsync(AggregateRoot agg);
        Task<T> GetByIdAsync(Guid id);
    }
}
