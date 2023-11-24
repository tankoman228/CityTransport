using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTransport.DB_Objects
{
    [Table("TransportType")]
    internal class TransportType
    {
        [Key]
        public int ID_TransportType { get; set; }  
        public string Name { get; set; }
    }
}
