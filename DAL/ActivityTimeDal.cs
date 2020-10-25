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
        public static activityTime GetActivityTimeById(int activityTimeId)
        {
            
            using (onLineEntities1 entities = new onLineEntities1())
            {
                return entities.activityTimes.FirstOrDefault(a => a.activityTimeId == activityTimeId);
            }
        }

        public static void updateActivityTime(activityTime activityTime)
        {
            try
            {
                using (onLineEntities1 entities = new onLineEntities1())
                {
                    var updateActivityTime = entities.activityTimes.FirstOrDefault(a => a.activityTimeId == activityTime.activityTimeId);
                    updateActivityTime.ActualDurationOfService = activityTime.ActualDurationOfService.Value;
                    updateActivityTime.StandardDeviation = activityTime.StandardDeviation.Value;
                    updateActivityTime.sampleSize=updateActivityTime.sampleSize.Value;
                    entities.SaveChanges();

                }

            }
            catch (Exception)
            {

                throw;
            } 
        }
    }
}
