using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BL
{
    public class TurnBL
    {
        public static bool IsAvailableHour(ref int index, int numOfWorkers, TimeSpan hour, List<customersInLine> line)
         {

            if (index >= line.Count() || line[index].statusTurn==1 && line[index].enterHour >= hour || line[index].statusTurn == 2 && line[index].estimatedHour.TimeOfDay >= hour)

            if (index >= line.Count() || line[index].estimatedHour.TimeOfDay >= hour&&line[index].statusTurn==(int)eStatus.IMMEDIATELY || line[index].enterHour >= hour && line[index].statusTurn == (int)eStatus.ADVANCE)

            {
                return true;
            }
            else//אם יש כבר תור בזמן זה 
                // בודק תור פנוי גם לפי מספר הקופות
            {
                int countTurnsForSameHour = 0;

                while (index < line.Count() &&( line[index].statusTurn == 1 && line[index].enterHour == hour || line[index].statusTurn == 2 && line[index].estimatedHour.TimeOfDay == hour))

                while (index < line.Count() && (line[index].estimatedHour.TimeOfDay >= hour && line[index].statusTurn == (int)eStatus.IMMEDIATELY || line[index].enterHour >= hour && line[index].statusTurn == (int)eStatus.ADVANCE))

                {
                    index++;
                    countTurnsForSameHour++;

                }
                if (countTurnsForSameHour < numOfWorkers)
                    return true;
            }
            return false;
        }


        public static void DeleteTurn(int turnId)
        {
            TurnDal.DeleteTurn(turnId);
        }
    
    public static string CreateVerificationCode(customersInLine turn)
    {
        string code = "" + turn.TurnId + turn.custId;
        return code;
    }
}
}
