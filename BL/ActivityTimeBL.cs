using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BL
{
    public class ActivityTimeBL
    {
        /// <summary>
        /// הפונקציה מקבלת שעה וקוד שירות ומחפשת את המשמרת בשירות הספציפי בהתאם לשעה שקיבלה
        /// </summary>
        /// <param name="time">שעה</param>
        /// <param name="serviceId">קוד שירות</param>
        /// <returns>קוד משמרת </returns>


        public static ActivityTimeDTO GetActivityTime(DateTime time, int serviceId)
        {
            List<ActivityTimeDTO> activityTimes = new List<ActivityTimeDTO>();
            activityTimes = converters.ActivityTimeConverters.GetListActivityTimesDTO(ActivityTimeDal.GetActivityTimes(serviceId));
            ActivityTimeDTO activityTime = activityTimes.FirstOrDefault(a => a.DayInWeek ==( (int)time.DayOfWeek+1) && a.StartTime <= time.TimeOfDay && a.EndTime > time.TimeOfDay && a.StartDate <= time &&(a.EndDate==null|| a.EndDate >= time));
            if (activityTime == null)
                activityTime = ActivityTimeBL.GetNearestActivityTime(time.TimeOfDay, serviceId);
            return activityTime;
            
        }

        public static ActivityTimeDTO GetNearestActivityTime(TimeSpan time, int serviceId)
        {
            List<ActivityTimeDTO> activityTimes = new List<ActivityTimeDTO>();
            activityTimes = converters.ActivityTimeConverters.GetListActivityTimesDTO(ActivityTimeDal.GetActivityTimes(serviceId));
            activityTimes=activityTimes.Where(a => a.DayInWeek == (int)DateTime.Now.DayOfWeek +1 && a.StartDate <= DateTime.Now && a.EndDate <= DateTime.Now && a.StartTime >= time).OrderBy(t => t.StartTime).Take(1).ToList();
            if (activityTimes.Count() == 0)
                return null;
            return activityTimes[0];
        }
    }
}
