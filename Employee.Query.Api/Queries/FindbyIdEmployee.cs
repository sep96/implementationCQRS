using CQRS.Core.Queries;

namespace Employee.Query.Api.Queries
{
    public class FindbyIdEmployee : BaseQuery
    {
        public Guid ID { get; set; }
    }
}
