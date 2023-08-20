using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Core.Exceptions
{
    public class AggregatenotFoundExceptions : Exception
    {
        public AggregatenotFoundExceptions(string message) : base(message) { }

    }
}
