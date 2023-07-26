using implementationCQRS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementationCQRS.Command
{
    public interface IEmployeeCommands
    {
        void SaveEmployeeData(Employee employee);
    }
}
