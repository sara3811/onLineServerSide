using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;
namespace BL.converters
{
    public class TurnInBusinessConverters
    {

        public static TurnInBusinessDTO GetTurnInBusinessDTO(service service)
        {

            TurnInBusinessDTO business = new TurnInBusinessDTO()
            {
                ServiceId = service.serviceId,
                BusinessName = service.business.businessName,
                Address = service.business.adress_street + " " + service.business.adress_numOfStreet + " " + service.business.adress_city,

            };
            return business;
        }

        public static List<TurnInBusinessDTO> GetTurnsInBusinessDTO(List<service> services)
        {
            List<TurnInBusinessDTO> businesses = new List<TurnInBusinessDTO>();
            services.ForEach(b => businesses.Add(GetTurnInBusinessDTO(b)));
            return businesses;

        }

        public static TurnInBusinessDTO GetTurnToShowDTO(customersInLine custTurn)
        {

            TurnInBusinessDTO turn = new TurnInBusinessDTO()
            {
                ServiceId = custTurn.activityTime.serviceId,
                BusinessName = custTurn.activityTime.service.business.businessName,
                Address = custTurn.activityTime.service.business.adress_street + " " + custTurn.activityTime.service.business.adress_numOfStreet + " " + custTurn.activityTime.service.business.adress_city,
                TurnId= custTurn.TurnId,
                EstimatedHour= custTurn.estimatedHour.TimeOfDay,

            };
            return turn;
        }

        public static List<TurnInBusinessDTO> GetTurnsToShowDTO(List<customersInLine> custTurns)
        {
            List<TurnInBusinessDTO> turns = new List<TurnInBusinessDTO>();
            custTurns.ForEach(t => turns.Add(GetTurnToShowDTO(t)));
            return turns;

        }
    }
}
