using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BL
{
    public class MakeAppointment
    {


        public static string BookAppointment(TurnDetailsDTO appointment)
        {

            try
            {
                ActivityTimeDTO activityTime = ActivityTimeBL.GetActivityTime(appointment.EstimatedHour, appointment.ServiceId);

                customersInLine turn = new customersInLine()
                {
                    activityTimeId = activityTime.ActivityTimeId,
                    custId = appointment.CustId,
                    estimatedHour = appointment.EstimatedHour,
                    preAlert = appointment.PreAlert,
                    statusTurn = (int)eStatus.ADVANCE,
                    enterHour = ConfigureHour(appointment.EstimatedHour, activityTime),
                     isActive=true
                };
                turn.TurnId = TurnDal.AddAppointment(turn);
                string verificationCode = TurnBL.CreateVerificationCode(turn);
                turn.verificationCode = verificationCode;
                TurnDal.UpdateTurn(turn);
                return verificationCode;
            }
            catch (Exception)
            {

                throw;
            }


        }




        public static List<DateTime> GetOptionalDaysPerService(int serviceId)
        {
            int day = (int)DateTime.Today.DayOfWeek + 1;
            DateTime date = DateTime.Now;
            List<activityTime> activityTimes = ActivityTimeDal.GetActivityTimes(serviceId);
            List<DateTime> optionalDays = new List<DateTime>();
            int limitDays = ServiceDal.GetServicById(serviceId).limitDays.Value;
            for (int i = 0; i < limitDays; i++, day++)
            {
                if (day == 7)
                {
                    day = 1;
                    i++;
                    limitDays++;
                }
                if (activityTimes.FirstOrDefault(a => a.dayInWeek == day) != null)
                {
                    optionalDays.Add(date.AddDays(i));
                }
            }
            return optionalDays;
        }

        public static List<TimeSpan> GetOptionalHoursPerDay(int serviceId, DateTime date)
        {
            List<activityTime> activityTimes = ActivityTimeDal.GetActivityTimesByDay(serviceId, (int)date.DayOfWeek + 1);
            List<TimeSpan> optionalHours = new List<TimeSpan>();
            int activityTimeIndex = 0;
            int index = 0;
            activityTimes.OrderBy(a => a.startTime);
            while (activityTimeIndex < activityTimes.Count())
            {
                activityTime activityTime = activityTimes[activityTimeIndex];
                List<customersInLine> line = TurnDal.GetLinePerActivityTime(activityTime.activityTimeId);
                double durationOfService = activityTime.actualServiceDuration.Value;
                TimeSpan ts = TimeSpan.FromMinutes(durationOfService);
                for (TimeSpan hour = activityTime.startTime; hour < activityTime.endTime; hour = hour.Add(ts))
                {
                    if (TurnBL.IsAvailableHour(ref index, activityTime.numOfWorkers, hour.Add(ts), line))
                    {
                        if (!date.Equals(DateTime.Today) || hour.Add(ts) > DateTime.Now.TimeOfDay)
                            optionalHours.Add(hour);
                    }
                    index++;
                }
                activityTimeIndex++;
            }
            return optionalHours;

        }
        /// <summary>
        /// פונקציה המחשבת את השעה עבור תור הנקבע מראש ומטרתה לחסוך המתנה בזמני עומס  
        /// </summary>
        /// <param name="date"></param>
        /// <param name="activityTime"></param>
        /// <returns>שעה לוגית  </returns>
        public static TimeSpan ConfigureHour(DateTime date, ActivityTimeDTO activityTime)
        {
            if (activityTime.AverageNumOfWaitingPeople == null)
                return date.TimeOfDay;
            TimeSpan logicHour = new TimeSpan();
            //todoever: לקבוע את המשתנה בהתאם לאמינות-לסטית תקן
            int numOfIgnoreServiceDuration = 3;
            //

            int numOfSub = (int)(activityTime.AverageNumOfWaitingPeople.Value / activityTime.ActualDurationOfService - numOfIgnoreServiceDuration);

            logicHour = date.TimeOfDay.Subtract(TimeSpan.FromMinutes(activityTime.ActualDurationOfService.Value * numOfSub));
            if (logicHour > activityTime.StartTime)
                return logicHour;
            else
                return activityTime.StartTime;

        }

    }
}
