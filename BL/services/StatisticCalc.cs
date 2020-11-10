using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BL.services
{
    public class StatisticCalc
    {
        const int minSequence = 2;
        const int minToDivide = 6;
        //parameters to determine if it is 
        const int weekly = 10;
        const int monthly = 5;
        const int yearly = 3;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="turn"></param>
        /// <param name="actualDuration"></param>
        /// <returns>if this turn is unusual</returns>
        private static bool IsSignificantDeviation(customersInLine turn, double actualDuration, bool IsServiceDuration)
        {
            double currentLen;
            if (IsServiceDuration == true)
                currentLen = (turn.exitHour.Value - turn.actualHour.Value).TotalMinutes;
            else
                currentLen = turn.numOfPushTimes.Value;

            if (currentLen * 1.3 < actualDuration || currentLen * 0.7 > actualDuration)
                return true;
            return false;

        }
        /// <summary>
        /// find sequence of unusual turns
        /// </summary>
        /// <param name="activityTimeId"></param>
        public static void findUnusualSequences(int activityTimeId, double avg, bool IsServiceDuration)
        {
            var line = DAL.TurnDal.GetLinePerActivityTime(activityTimeId);
            List<int> significantDeviationIndexes = new List<int>();
            DAL.activityTime activityTime = ActivityTimeDal.GetActivityTimeById(activityTimeId);
            int i = 0;
            bool isSignificantStatus = false;
            //שומרת ברשימה את האינדקסים של כל התחלה וסוף של תורים חריגים
            foreach (var item in line)
            {
                bool isSignificant = IsSignificantDeviation(item, avg, IsServiceDuration);
                if (isSignificant && !isSignificantStatus)
                {
                    significantDeviationIndexes.Add(i);
                    isSignificantStatus = true;
                }
                else if (!isSignificant && isSignificantStatus)
                {
                    significantDeviationIndexes.Add(i - 1);
                    isSignificantStatus = false;
                }

                i++;
            }

            int sum = 0;
            int index = 0;
            // עוברת על מערך האינדקסים ובודקת האם הרצף משמעותי -אם כן ממשיכה ובודקת האם קים הפרש משמעותי בינו לבין הרצף הבא
            //אם כן ממשיכה לסכום- אם לא בודקת האם זהו רצף ארוך מספיק כדי להכניסו לטבלת חריגים

            for (i = 1; i < significantDeviationIndexes.Count; i += 2)
            {
                int length = significantDeviationIndexes[i] - significantDeviationIndexes[i - 1];
                if (length > minSequence)
                {
                    sum += length;
                    if (index == 0)
                        index = significantDeviationIndexes[i - 1];
                }
                if (significantDeviationIndexes[i + 1] - significantDeviationIndexes[i] > minSequence)
                {/*
                    if (sum >= minToDivide)
                        //todo: SetUnusualActivityTime(line.Skip(index).Take(significantDeviationIndexes[i]).ToList());
                    else
                      //todo:  UpdateStatistics(line.Skip(index).Take(significantDeviationIndexes[i]).ToList(), activityTime);
                    index = 0;
                    sum = 0;
                    */
                }
            }


        }
        /// <summary>
        /// calc the new data with the old statisic
        /// </summary>
        /// <param name="line">the turns that are not unuaual</param>
        /// <param name="activityTime"></param>
        private static void UpdateStatistics(List<customersInLine> line, activityTime activityTime, bool IsServiceDuration)
        {
            //todoever: לעשות ביטוי למבדה במקום ה-if
            //שליפת ממוצע משמרת הכפלה ברוחב המדגם הוספת הנתונים החדשים, הוספת מספר הנתונים לרוחב המדגם וחלוקה של הסכום ברוחב המדגם
            //חישוב מחודש של סטיית טקן
            int totalSampleSize = activityTime.sampleSize.Value + line.Count();
            double activityTimeAvg, newAvg, weightedAverage;
            if (IsServiceDuration)
            {
                activityTimeAvg = activityTime.actualServiceDuration.Value;
                newAvg = line.Average(t => (t.exitHour.Value - t.actualHour.Value).TotalMinutes);
            }
            else
            {
                activityTimeAvg = activityTime.avgWaitings.Value;
                newAvg = line.Average(t => t.numOfPushTimes.Value);
            }
            weightedAverage = (activityTimeAvg * activityTime.sampleSize.Value + newAvg * line.Count()) / totalSampleSize;
            double newStandardDeviation = calcStandartDeviation(line, IsServiceDuration);
           //todo: double weightedStandardDeviation = (activityTime.sampleSize.Value * Math.Sqrt(activityTime.sStandardDeviation.Value) + line.Count() * Math.Sqrt(newStandardDeviation));
          //  weightedStandardDeviation += Math.Sqrt(activityTime.sampleSize.Value * (activityTimeAvg - weightedAverage)) + Math.Sqrt(line.Count() * (newAvg - weightedAverage));
           // weightedStandardDeviation /= totalSampleSize;
           //todo: להוציא שורש מכל הסיפור הזה כי זה שונות משוקללת  ולא סטית תקן
            activityTime.sampleSize = totalSampleSize;
            if (IsServiceDuration)
            {
                activityTime.actualServiceDuration = weightedAverage;
              //  activityTime.serviceStandardDeviation = weightedStandardDeviation;
            }
            else
            {
                activityTime.avgWaitings = weightedAverage;
             //   activityTime.waitingStandardDeviation = weightedStandardDeviation;
            }
            ActivityTimeDal.updateActivityTime(activityTime);
        }

        private static void SetUnusualActivityTime(List<customersInLine> unusalLine, bool IsServiceDuration)
        {
            DAL.unusual unusual;
            int activityTimeId = unusalLine.ToList()[0].activityTimeId;
            double standartDeviation= calcStandartDeviation(unusalLine,IsServiceDuration);
          if (IsServiceDuration)
            {
                 unusual = new unusual(activityTimeId, unusalLine.Average(t => (t.exitHour.Value - t.actualHour.Value).TotalMinutes),
                 true,
                 unusalLine[0].estimatedHour.Add(unusalLine[0].actualHour.Value - unusalLine[0].estimatedHour.TimeOfDay),
                 unusalLine[unusalLine.Count() - 1].exitHour.Value,
                 standartDeviation);
            }
            else
            {
                unusual = new unusual(activityTimeId, unusalLine.Average(t => t.numOfPushTimes.Value),
               false,
               unusalLine[0].estimatedHour,
               unusalLine[unusalLine.Count() - 1].exitHour.Value,
               standartDeviation);

            }
                      UnusualDal.AddUnUsual(unusual);
            scanUnusuals(unusual);

        }
     

        static double calcStandartDeviation(List<customersInLine> line, bool IsServiceDuration)
        {


            double ret = 0;
            int count = line.Count();
            if (count > 1)
            {
                double sum,avg;
                //Perform the Sum of (value-avg)^2
                if (IsServiceDuration)
                {
                  avg= line.Average(t => (t.exitHour.Value - t.actualHour.Value).TotalMinutes);
                    sum = line.Sum(t => ((t.exitHour.Value - t.actualHour.Value).TotalMinutes - avg) * ((t.exitHour.Value - t.actualHour.Value).TotalMinutes - avg));
                }

                else

                {
                    avg= line.Average(t => t.numOfPushTimes.Value);
                    sum = line.Sum(t => ((t.numOfPushTimes.Value - avg) * (t.numOfPushTimes.Value - avg))); }
                //Put it all together
                ret = Math.Sqrt(sum / count);
            }
            return ret;
        }
        public static void calcAvgServiceDuration(int activityTimeId)
        {
            DAL.activityTime activityTime = ActivityTimeDal.GetActivityTimeById(activityTimeId);
            double avg = activityTime.actualServiceDuration.Value;
            findUnusualSequences(activityTimeId, avg, true);
        }

        public static void calcAvgWaitingPeople(int activityTimeId)
        {
            //ההבדל בין הזמן המשוער לזמן האמיתי
            //הנתון המענין כמה דחיפות היו בזמן הזה צריך לדעת ממוצע דחיפות
            //numOfPushTimes לפי
            //מאד דומה לפונקציה למעלה
            //todo: כשמאתחלים משמרת חדשה לאפס נתונים
            DAL.activityTime activityTime = ActivityTimeDal.GetActivityTimeById(activityTimeId);
            double avg = activityTime.avgWaitings.Value;
            findUnusualSequences(activityTimeId, avg, false);

        }

        private static void scanUnusuals(DAL.unusual unusual)
        {
            //כרגע אין פה התיחסות לחריגות של משמרות מיוחדות
            var servicesUnusuals = DAL.UnusualDal.GetUnusuals(unusual.activityTime.serviceId).Where(u => u.kindOfUnusual == unusual.kindOfUnusual);
            var weeklyUnusuals = servicesUnusuals.Where(u => u.activityTime.dayInWeek == unusual.activityTime.dayInWeek).ToList();
            var monthlyyUnusuals = servicesUnusuals.Where(u => u.startTime.Value.Day == unusual.startTime.Value.Day);
            var yearUnusuals = servicesUnusuals.Where(u => u.startTime.Value.DayOfYear == unusual.startTime.Value.DayOfYear);
            //todo: לטפל בחריגות הפוכה
            //חריגות הפוכה
            // weeklyUnusuals = servicesUnusuals.Where(u => u.activityTime.dayInWeek == unusual.activityTime.dayInWeek).ToList();
            // monthlyyUnusuals = servicesUnusuals.Where(u => DateTime.Today.Day == DateTime.Today.Day);
            // yearUnusuals = servicesUnusuals.Where(u => u.activityTime.dayInWeek == unusual.activityTime.dayInWeek);

            if (weeklyUnusuals.Count() > 10)
            {
                TimeSpan startTime = weeklyUnusuals.Max(u => u.startTime.Value.TimeOfDay);
                TimeSpan endTime = weeklyUnusuals.Min(u => u.endTime.Value);

                for (int i = 0; i < weeklyUnusuals.Count(); i++)
                {

                    if (weeklyUnusuals.ElementAt(i).startTime.Value.TimeOfDay > endTime ||
                        weeklyUnusuals.ElementAt(i).endTime.Value < startTime)
                        weeklyUnusuals.RemoveAt(i--);

                }
                if (weeklyUnusuals.Count() > 10)
                {

                }




            }




        }



    }

}
