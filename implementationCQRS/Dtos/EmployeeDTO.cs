    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace implementationCQRS.Dtos
{
    public class EmployeeDTO
    {
        public int Id
        {
            get;
            set;
        }
        public string FullName
        {
            get;
            set;
        }
        public int Age
        {
            get;
            set;
        }
        public string Address
        {
            get;
            set;
        }
        public string RegistrationDate
        {
            get;
            set;
        }
    }
}
