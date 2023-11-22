using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTransport.DB_Objects
{
    [Table("RouteStop")]
    internal class RouteStop
    {
        [Key]
        public int ID_RouteStop { get; set; }

        public int ID_Route { get; set; }
        public int ID_Stop { get; set; }

        [ForeignKey("ID_Stop")]
        public Stop Stop { get; set; }

        public short NumInWay { get; set; }
        public decimal DistanceToPreviousKm { get; set; }
    }
}
