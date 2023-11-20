using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTransport.Forms.Items
{
    internal class WorkerCbItem
    {
        public DB_Objects.Worker W { get; set; }

        public override string ToString()
        {
            return $"{W.Email} : {W.Sirname} : {W.Phone}";
        }
    }
}
