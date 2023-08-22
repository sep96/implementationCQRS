using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Employee.Query.Domain.Entity
{
    [Table("Vacation")]
    public class VacationEntity
    {
        [Key]
        public Guid VacationId { get; set; }
        public int TotalDays { get; set; }

        public DateTime StartDate { get; set; }
        [JsonIgnore]
        public virtual EmployeeEntity Employee { get; set; }

    }
}
