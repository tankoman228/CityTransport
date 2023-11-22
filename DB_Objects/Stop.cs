using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTransport.DB_Objects
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Stop")]
    internal class Stop
    {
        [Key]
        public int ID_Stop { get; set; }
        public int ID_District { get; set; }
        public string Name { get; set; }

        public DbGeography Place { get; set; }
        public bool HasPavilion { get; set; }
    }

}
