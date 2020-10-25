using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BL
{

    //todo: לשנות את שם המחלקה ולהחליט איפה יהיו הפונקציות
    public class Cashier

    {
        public static TurnDetailsDTO GetNearestTurn(int serviceId)
        {
            try
            {
                ActivityTimeDTO activityTime = ActivityTimeBL.GetActivityTime(DateTime.Now, serviceId);
                List<customersInLine> line = new List<customersInLine>();
                if (activityTime != null)
                {
                    line = TurnDal.GetLinePerActivityTime(activityTime.ActivityTimeId);
                    if (line.Count() == 0)
                        throw new Exception("there is no turns now");
                }

                //todo: לא גמור צריך לבדוק את המצב של כמה עובדים במשמרת
                else
                    throw new Exception("there is no activityTime now");
                return converters.TurnDetailsConverters.GetTurnDetailsDTO(line[0]);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
/// <summary>
/// occured whenever turn finished
/// </summary>
/// <param name="turn"></param>
        public static void CompleteTurn(TurnDetailsDTO turn)
        {

            try
            {
                customersInLine acceptedTurn = TurnDal.GetTurnByTurnId(turn.TurnId);
                acceptedTurn.ActualHour = DateTime.Now.TimeOfDay;
                TurnDal.UpdateTurn(acceptedTurn);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// occured when-ever activityTime is over
        /// </summary>
        /// <param name="activityTimeId"></param>
        public static void CompleteActivityTime(int activityTimeId)
        {
            BL.services.StatisticCalc.calcAvgServiceDuration(activityTimeId);
            BL.services.StatisticCalc.calcAvgWaitingPeople();
            var line = TurnDal.GetLinePerActivityTime(activityTimeId);
            line.ForEach(t => t.isActive = false);
            DAL.TurnDal.UpdateTurns(line);
         
        }
    }
}
