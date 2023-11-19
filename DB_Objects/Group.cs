using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTransport.DB_Objects
{
    [Table("Group")]
    internal class Group
    {
        [Key]
        public int ID_Group { get; set; }
        
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
