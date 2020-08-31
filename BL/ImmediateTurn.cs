using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
using System.Runtime.InteropServices;
using BL.converters;

namespace BL
{
    public class ImmediateTurn
    {
        //toask: סטטי או מופעים של המחלקות



        /// <summary>
        /// push the turns till the end of the activity time or stop if an empty time was found
        /// </summary>
        /// <param name="line">רשימת התורים שבהם אמורה להתבצע הדחיפה</param>
        /// <param name="time">שעת התור</param>
        /// <param name="activityTime"></param>
        /// <returns>num of pushed turns</returns>
        private static int pushTurns(List<customersInLine> line, TimeSpan time, ActivityTimeDTO activityTime)
        {
            TimeSpan ts = TimeSpan.FromMinutes((double)activityTime.ActualDurationOfService);
            int pushedTurnsCnt = 0;

            for (int i = 0; i < line.Count(); i++)
            {
                if ((line[i].statusTurn == (int)eStatus.IMMEDIATELY && line[i].estimatedHour.TimeOfDay >= time && line[i].estimatedHour.TimeOfDay < time.Add(ts)) || (line[i].statusTurn == (int)eStatus.ADVANCE && line[i].enterHour >= time && line[i].enterHour < time.Add(ts)))
                {
                    pushedTurnsCnt++;
                    if (line[i].statusTurn == (int)eStatus.IMMEDIATELY)
                    {
                        line[i].estimatedHour = line[i].estimatedHour.Add(ts);
                        time = line[i].estimatedHour.TimeOfDay;
                    }
                    else
                    {
                        line[i].enterHour = line[i].enterHour.Value.Add(ts);
                        time = line[i].enterHour.Value;
                    }

                }
                else
                    break;
            }
            return pushedTurnsCnt;
        }




        private static TimeSpan? lookForAvailableTurn(ActivityTimeDTO activityTime, ref bool pushFlag, TimeSpan timeToLookFor)
        {
            List<customersInLine> line = TurnDal.GetLinePerActivityTime(activityTime.ActivityTimeId);
            pushFlag = false;
            double durationOfService = (double)activityTime.ActualDurationOfService;
            //מחשב את ההפרש בין זמן התחלת המשמרת לזמן שקבלנו בפרמטר ע"מ למצוא את הזמן שבו יתחיל לחפש תור 
            int totalPassedShifts = (int)((timeToLookFor.TotalMinutes - activityTime.StartTime.TotalMinutes) / durationOfService) + 1;
            // לקבוע פרמטר של פנוי שיהיה לפי ממוצע זמן ההמתנה
            const int maxStandbyTime = 3;
            const int maxPushedTimes = 2;
            TimeSpan hour;
            TimeSpan ts = TimeSpan.FromMinutes(durationOfService * totalPassedShifts);
            int index = 0, numOfStandbyTime = 0;
            timeToLookFor = activityTime.StartTime.Add(ts);
            line = line.Where(t => t.estimatedHour.TimeOfDay >= timeToLookFor).ToList();
            if (line.Count() == 0 && timeToLookFor < activityTime.EndTime)
                //todoever: לבדוק חוקיות עסקים -כלומר שהם קימים באמת
                return timeToLookFor;
            ts = TimeSpan.FromMinutes(durationOfService);
            for (hour = timeToLookFor; numOfStandbyTime < maxStandbyTime && hour < activityTime.EndTime; hour = hour.Add(ts), numOfStandbyTime++)
            {
                //todoever: לבדוק גם מקרים של מרווחי זמן הקטנים מזמן השירות- להתייחס לקביעת תור עם אפשרות לכל זמן ולא דווקא בזמנים קבועים
                if (TurnBL.IsAvailableHour(ref index, activityTime.NumOfWorkers, hour.Add(ts), line))
                    return hour;
                index++;
            }
            if (hour >= activityTime.EndTime)
            {
                activityTime = ActivityTimeBL.GetNearestActivityTime(hour, activityTime.ServiceId);
                if (activityTime == null)
                    return null;
                return lookForAvailableTurn(activityTime, ref pushFlag, activityTime.StartTime);
            }
            pushFlag = true;
            while (index < line.Count() && line[index].estimatedHour.TimeOfDay == hour && line[index].numOfPushTimes == maxPushedTimes)
            {
                index++;
                hour = hour.Add(ts);
            }
            //todo: לבדוק את הלולאות ותקינות ההחזרה
            return hour;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns>optional hour for immidate turn for the required business</returns>
        public static TimeSpan GetOptionalHourPerBusiness(int serviceId, TimeSpan timeToLookFor, ref bool pushFlag)
        {
            //to ask: ... לכתוב את החילוץ פעמיים 
            DateTime time = DateTime.Now;
            time = time.Add(timeToLookFor - time.TimeOfDay);
            ActivityTimeDTO activityTime = ActivityTimeBL.GetActivityTime(time, serviceId);
            if (activityTime == null)
                return new TimeSpan();
            return (TimeSpan)lookForAvailableTurn(activityTime, ref pushFlag, timeToLookFor);
        }






        public static string ConfirmImmediateTurn(TurnDetailsDTO turn)
        {
            List<customersInLine> line = new List<customersInLine>();


            customersInLine newTurn = TurnDal.GetTurnByTurnId(turn.TurnId);
            line = TurnDal.GetLineByCustomer(newTurn.custId).Where(l => l.statusTurn == (int)eStatus.TEMPORARY || l.statusTurn == (int)eStatus.TEMPORARY_WITH_PUSH).ToList();
            var x = line.Remove(line.First(t => t.TurnId == turn.TurnId));
            if (newTurn.statusTurn == (int)eStatus.TEMPORARY_WITH_PUSH)
                pushTurns(TurnDal.GetLinePerActivityTime(newTurn.activityTime.serviceId), newTurn.estimatedHour.TimeOfDay, ActivityTimeConverters.GetActivityTimeDTO(newTurn.activityTime));
            newTurn.statusTurn = (int)eStatus.IMMEDIATELY;
            string verificationCode = TurnBL.CreateVerificationCode(newTurn);
            newTurn.verificationCode = verificationCode;
            newTurn.preAlert = turn.PreAlert;
            TurnDal.UpdateTurn(newTurn);
            if (line.Count() > 0)
                line.ForEach(a => TurnDal.DeleteTurn(a.TurnId));
            return verificationCode;
        }



        /// <summary>
        /// הפונקציה קובעת תור זמני עבור שעה אופציונלית בעסק 
        /// </summary>
        /// <param name="turn"></param>
        /// <returns>turn id</returns>
        public static int MakeTemporaryTurn(TurnInBusinessDTO turn, bool pushFlag, int custId)
        {
            DateTime time = DateTime.Now;
            ActivityTimeDTO activityTime = ActivityTimeBL.GetActivityTime(time.Add(turn.EstimatedHour.Value - time.TimeOfDay), turn.ServiceId);

            if (activityTime == null)
                throw new Exception("אין משמרת פעילה כרגע");

            //todoever: להחליף בהמשך לאינדקסים
            try
            {
                int status = (int)eStatus.TEMPORARY;
                if (pushFlag)
                    status = (int)eStatus.TEMPORARY_WITH_PUSH;
                CustomerInLineDTO temporaryTurn = new CustomerInLineDTO()
                {
                    ActivityTimeId = activityTime.ActivityTimeId,
                    CustId = custId,
                    EnterHour = TimeSpan.FromMinutes(turn.Duration).Add(DateTime.Now.TimeOfDay),
                    EstimatedHour = DateTime.Today.Add(turn.EstimatedHour.Value),

                    PreAlert = 0,
                    StatusTurn = status
                };

                int turnId = TurnDal.AddAppointment(converters.CustomerInLineConvrters.GetCustomerInLine(temporaryTurn));
                return turnId;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
