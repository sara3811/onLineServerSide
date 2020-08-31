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
                Address = service.business.Adress_street + " " + service.business.Adress_numOfStreet + " " + service.business.Adress_city,

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
                Address = custTurn.activityTime.service.business.Adress_street + " " + custTurn.activityTime.service.business.Adress_numOfStreet + " " + custTurn.activityTime.service.business.Adress_city,
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
