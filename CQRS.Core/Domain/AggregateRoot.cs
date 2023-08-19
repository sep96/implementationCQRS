using CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Domain
{
    public abstract class AggregateRoot
    {
        protected Guid _id;
        readonly List<BaseEvent> _changes = new();
        public Guid Id
        {
            get { return _id; }
        }
        public int Version { get; set; } = -1;
        public IEnumerable<BaseEvent> getUncommitedChanges { get { return _changes;  } }
        public void MarkchangesAsCommit()
        {
            _changes.Clear();
        }
        private void ApplyChanges(BaseEvent @event , bool isNew)
        {
            var method = this.GetType().GetMethod("Apply", new Type[] { @event.GetType() });
            if (method is null)
                throw new ArgumentException(nameof(method), $"the Apply method not found");
            method.Invoke(this, new object[] { @event });
            if (isNew)
                _changes.Add(@event);

        }
        protected void RaiseEvent(BaseEvent @event)
        {
            ApplyChanges(@event, true);
        }
        public void RelayEvent(IEnumerable<BaseEvent> events)
        {
            foreach(var eve in events)
            {
                ApplyChanges(eve, true);
            }
        }
    }
}
