using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTransport.DB_Objects
{
    [Table("Post")]
    internal class Post
    {
        [Key]
        public int ID_Post { get; set; }
        public string Name { get; set; }
    }
}
