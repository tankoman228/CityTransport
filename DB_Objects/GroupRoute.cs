using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTransport.DB_Objects
{
    [Table("GroupRoute")]
    internal class GroupRoute
    {
        [Key]
        public int ID_GroupRoute { get; set; }
        public int ID_Group { get; set; }
        public int ID_Route { get; set; }

        [ForeignKey("ID_Route")]
        public Route Route { get; set; }
    }
}
