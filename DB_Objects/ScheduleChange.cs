using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTransport.DB_Objects
{
    [Table("ScheduleChange")]
    internal class ScheduleChange
    {
        [Key]
        public int ID_ScheduleChange { get; set; }

        public int ID_RouteSchedule { get; set; }

        [ForeignKey("ID_RouteSchedule")]
        public RouteSchedule RouteSchedule { get; set; }

        public DateTime NewTime { get; set; }

        public string Reason { get; set; }
    }
}
