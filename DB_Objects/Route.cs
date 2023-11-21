using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTransport.DB_Objects
{
    [Table("Route")]
    internal class Route
    {
        [Key]
        public int ID_Route { get; set; }


        public int ID_Carrier { get; set; }
        [ForeignKey("ID_Carrier")]
        public Carrier Carrier { get; set; }

        public int ID_TransportType { get; set; }
        public string RouteNumber { get; set; }
        public int TimeInWayMinutes { get; set; }

        public bool onMonday { get; set; }
        public bool onTuesday { get; set; }
        public bool onWednesday { get; set; }
        public bool onThursday { get; set; }
        public bool onFriday { get; set; }
        public bool onSaturday { get; set; }
        public bool onSunday { get; set; }
    }
}
