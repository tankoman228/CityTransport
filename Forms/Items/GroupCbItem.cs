using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTransport.Forms.Items
{
    internal class GroupCbItem
    {
        public DB_Objects.Group G { get; set; }

        public override string ToString()
        {
            return G.Name;
        }
    }
}

