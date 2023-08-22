using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Query.Domain.Entity
{
    [Table("Employee")]
    public  class EmployeeEntity
    {
        [Key]
        public Guid EmmployeeID { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public DateTime CreatedTime { get; set; }
        public int DayofWorks { get; set; }
        public virtual ICollection<VacationEntity> Vacations { get; set; }

    }
}
