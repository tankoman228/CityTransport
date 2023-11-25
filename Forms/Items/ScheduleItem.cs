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
        public DB_Objects.ScheduleChange C;

        public override string ToString()
        {
            if (C != null)
                return $"{S.RouteStop.Stop.Name}: {S.ArriveTime} => {C.NewTime} | {C.Reason}";
            return $"{S.RouteStop.Stop.Name} : {S.ArriveTime} >< {S.DepartureTime}";
        }
    }
}
