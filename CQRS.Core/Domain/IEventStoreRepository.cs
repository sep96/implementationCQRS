﻿using CQRS.Core.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Domain
{
    public interface IEventStoreRepository
    {
        Task SaveAsync(EventModel @event);
        Task<List<EventModel>> FindByAggregateIdAsync(Guid AggregateId);


    }
}
