using CQRS.Core.Infrastuctur;
using CQRS.Core.Queries;
using Employee.Query.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Employee.Query.Infrastructure.Dispatcher
{
    public class QueryDispatcher : IQueryDispatcher<EmployeeEntity>
    {
        private readonly Dictionary<Type, Func<BaseQuery, Task<List<EmployeeEntity>>>> _queryHandler = new();
        public void RegisterHandler<TQuery>(Func<TQuery, Task<List<EmployeeEntity>>> handler) where TQuery : BaseQuery
        {
            if (_queryHandler.ContainsKey(typeof(TQuery)))
                throw new Exception("You Can not Register Same kkey(Queryhandler) TWICE!!");
            else
                _queryHandler.Add(typeof(TQuery), x => handler((TQuery)x));
        }

        public async Task<List<EmployeeEntity>> SendAsync(BaseQuery query)
        {
            if (_queryHandler.TryGetValue(query.GetType(), out Func<BaseQuery, Task<List<EmployeeEntity>>> handler))
                return await handler(query);
            throw new ArgumentNullException("No query reigster");
        }
    }
}
