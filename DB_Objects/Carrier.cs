using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTransport.DB_Objects
{
    [Table("Carrier")]
    internal class Carrier
    {
        [Key]
        public int ID_Carrier { get; set; }

        public string Head { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
    }
}
