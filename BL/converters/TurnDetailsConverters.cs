using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
namespace BL.converters
{
    class TurnDetailsConverters
    {
        public static TurnDetailsDTO GetTurnDetailsDTO(customersInLine turn)
        {
            TurnDetailsDTO turnDetails = new TurnDetailsDTO
            {
                CustId = turn.custId,
                EstimatedHour = turn.estimatedHour,
                TurnId = turn.TurnId,
                VeriverificationCode = turn.verificationCode,
                ServiceId=turn.activityTime.serviceId,
                ActualHour=turn.actualHour.Value
                 //ActualHour=new TimeSpan()

            };
            return turnDetails;

        }
        
      public static customersInLine GetCustomersInLine(TurnDetailsDTO turnDTO)
        {
            customersInLine turn = new customersInLine()
            {
                custId = turnDTO.CustId,
                estimatedHour = turnDTO.EstimatedHour,
                actualHour = turnDTO.ActualHour,
                TurnId = turnDTO.TurnId,
                verificationCode = turnDTO.VeriverificationCode
            };
            return turn;
        }
    }
}
