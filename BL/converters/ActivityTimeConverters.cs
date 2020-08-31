using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.converters
{
    class ActivityTimeConverters
    {
        public static DAL.activityTime GetActivityTime(DTO.ActivityTimeDTO activityTimeDTO)
        {
            DAL.activityTime activityTime = new DAL.activityTime()
            {
                activityTimeId = activityTimeDTO.ActivityTimeId ,
                serviceId = activityTimeDTO.ServiceId ,
                dayInWeek = activityTimeDTO.DayInWeek ,
                startTime = activityTimeDTO.StartTime ,
                endTime = activityTimeDTO.EndTime ,
                numOfWorkers = activityTimeDTO.NumOfWorkers ,
                estimatedDurationOfService = activityTimeDTO.EstimatedDurationOfService ,
                ActualDurationOfService = activityTimeDTO.ActualDurationOfService,
                StandardDeviation = activityTimeDTO.StandardDeviation ,
                averageNumOfWaitingPeople = activityTimeDTO.AverageNumOfWaitingPeople ,
            };
            return activityTime;
        }

        public static DTO.ActivityTimeDTO GetActivityTimeDTO(DAL.activityTime activityTime)
        {
            DTO.ActivityTimeDTO activityTimeDTO = new DTO.ActivityTimeDTO()
            {
                ActivityTimeId = activityTime.activityTimeId ,
                ServiceId = activityTime.serviceId ,
                DayInWeek = activityTime.dayInWeek ,
                StartTime = activityTime.startTime ,
                EndTime = activityTime.endTime ,
                StartDate =  activityTime.startDate ,
                EndDate = activityTime.endDate ,
                NumOfWorkers =  activityTime.numOfWorkers ,
                EstimatedDurationOfService = activityTime.estimatedDurationOfService ,
                ActualDurationOfService = activityTime.ActualDurationOfService ,
                StandardDeviation = activityTime.StandardDeviation ,
                AverageNumOfWaitingPeople = activityTime.averageNumOfWaitingPeople ,
            };
            return activityTimeDTO;
        }

        public static List<ActivityTimeDTO> GetListActivityTimesDTO(List<DAL.activityTime> lActivityTimes)
        {
            List<ActivityTimeDTO> l = new List<ActivityTimeDTO>();
            lActivityTimes.ForEach(b => l.Add(GetActivityTimeDTO(b)));
            return l;
        }

        public static List<DAL.activityTime> GetListActivityTimes(List<ActivityTimeDTO> lActivityTime)
        {
            List<DAL.activityTime> l = new List<DAL.activityTime>();
            lActivityTime.ForEach(b => l.Add(GetActivityTime(b)));
            return l;
        }
    }
}
