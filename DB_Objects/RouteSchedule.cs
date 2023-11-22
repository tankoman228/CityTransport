using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTransport.DB_Objects
{
    [Table("RouteSchedule")]
    internal class RouteSchedule
    {
        [Key]
        public int ID_RouteSchedule { get; set; }
        public int ID_RouteStop { get; set; }

        [ForeignKey("ID_RouteStop")]
        public RouteStop RouteStop { get; set; }

        public TimeSpan ArriveTime { get; set; }
        public TimeSpan DepartureTime { get; set; }
    }
}
