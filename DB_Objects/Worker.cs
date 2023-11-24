using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTransport.DB_Objects
{
    [Table("Worker")]
    internal class Worker
    {
        [Key]
        public int ID_Worker { get; set; }

        public int ID_Post { get; set; }

        [ForeignKey("ID_Post")]
        public Post Post { get; set; }

        public int? ID_Carrier { get; set; }

        [ForeignKey("ID_Carrier")]
        public Carrier Carrier { get; set; }

        public string FirstName { get; set; }
        public string Sirname { get; set; }
        public string LastName { get; set; }

        public string Phone { get; set; }
        public string Email { get; set; }

        public char Gender { get; set; }

    }
}
