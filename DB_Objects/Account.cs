using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTransport.DB_Objects
{
    [Table("Account")]
    internal class Account
    {
        [Key]
        public int ID_Account { get; set; }

        public int? ID_Worker { get; set; }

        [ForeignKey("ID_Worker")]
        public Worker Worker { get; set; }

        public int? ID_Group { get; set; }
        public int ID_AccountType { get; set; }

        [ForeignKey("ID_Group")]
        public Group Group { get; set; }

        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public bool Status { get; set; }
    }
}
