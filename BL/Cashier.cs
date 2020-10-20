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
        public static void CompleteActivityTime(int activityTimeId)
        {
            //call to calcAvg x2
            var line = TurnDal.GetLinePerActivityTime(activityTimeId);
            //todo: send to update every turn to be not active
        }
    }
}
