using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTransport.Forms.Items
{
    internal class AccountLbItem
    {
        public DB_Objects.Account A { get; set; }

        public override string ToString()
        {
            return $"   {A.Username} : {A.Worker.FirstName} {A.Worker.Sirname} : {A.Worker.Email}";
        }
    }
}
