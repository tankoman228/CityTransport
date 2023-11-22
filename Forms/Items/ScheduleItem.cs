using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityTransport.Forms.Items
{
    internal class ScheduleItem
    {
        public DB_Objects.RouteSchedule S;

        public override string ToString()
        {
            return $"{S.RouteStop.Stop.Name} : {S.ArriveTime} >< {S.DepartureTime}";
        }
    }
}
