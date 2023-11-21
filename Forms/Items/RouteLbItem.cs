using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTransport.Forms.Items
{
    internal class RouteLbItem
    {
        internal DB_Objects.Route R;

        public override string ToString()
        {
            return $"{R.RouteNumber} ({R.Carrier.Name})";
        }
    }
}
