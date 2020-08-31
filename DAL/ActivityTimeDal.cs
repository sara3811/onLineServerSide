using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ActivityTimeDal
    {
        public static List<activityTime> GetActivityTimes(int serviceId)
        {
            using (onLineEntities1 entities=new onLineEntities1())
            {
                return entities.activityTimes.Where(a => a.serviceId == serviceId).ToList();
            }
        }

        public static List<activityTime> GetActivityTimesByDay(int serviceId,int day)
        {
            using(onLineEntities1 entities = new onLineEntities1())
            {
                return entities.activityTimes.Where(a => a.serviceId == serviceId && a.dayInWeek==day&&a.endDate==null).ToList();
            }
        }

    }
}
