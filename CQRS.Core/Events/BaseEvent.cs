using CQRS.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Events
{
    public abstract class BaseEvent : Message
    {

        protected BaseEvent(string type)
        {
            this.Type = type;
        }

  

        //User version when  we replay the last state of aggregate
        public int Version { get; set; }
        // discriminator property that we will when Data Binding. 
        public string Type { get; set; }
         
    }
}
