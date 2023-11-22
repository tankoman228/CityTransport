using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTransport.Forms.Items
{
    internal class StopItem
    {
        public DB_Objects.Stop S;
        public DB_Objects.RouteStop RS; //MAY BE NULL!

        public override string ToString()
        {
            return S.Name;
        }
    }
}
