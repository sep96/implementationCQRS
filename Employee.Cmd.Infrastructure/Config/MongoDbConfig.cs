using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Cmd.Infrastructure.Config
{
    public class MongoDbConfig
    {
        public string ConnectionString { get; set; }
        public string DataBase { get; set; }
        public string CollectionName { get; set; }
    }
}
