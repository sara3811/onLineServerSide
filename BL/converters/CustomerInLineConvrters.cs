using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.converters
{
    public class CustomerInLineConvrters
    {
        #region convert customersInLine to TurnDetailsDTO לא בטוח שבאמת צריך את ההמרה הזו
        /*
        public static TurnDetailsDTO GetTurnDetailsDTO(customersInLine appointment)
        {
            
            TurnDetailsDTO turnDetails = new TurnDetailsDTO()
            {
               ServiceId=appointment.activityTime.serviceId,
               PreAlert=appointment.preAlert,
               TurnId=appointment.TurnId
            };
            return turnDetails;
        }

        public static List<TurnDetailsDTO> GetTurnsDetailsDTO(List<customersInLine> appointments)
        {
            List<TurnDetailsDTO> line = new List<TurnDetailsDTO>();
            appointments.ForEach(a => line.Add(GetTurnDetailsDTO(a)));
            return line;
        }
        */
        #endregion

        #region convert customersInLine to CustomerInLineDTO
        public static CustomerInLineDTO GetCustomerInLineDTO(customersInLine appointment)
        {
            CustomerInLineDTO turnDetails = new CustomerInLineDTO()
            {
                ActivityTimeId=appointment.activityTimeId,
                StatusTurn=appointment.statusTurn,
                CustId = appointment.custId ,
                PreAlert = appointment.preAlert ,
                TurnId = appointment.TurnId,
                EstimatedHour=appointment.estimatedHour
            };
            turnDetails.EstimatedHour.Add(appointment.estimatedHour.TimeOfDay);
            return turnDetails;
        }

        public static List<CustomerInLineDTO> GetCustomersInLineDTO(List<customersInLine> appointments)
        {
            List<CustomerInLineDTO> line = new List<CustomerInLineDTO>();
            appointments.ForEach(a => line.Add(GetCustomerInLineDTO(a)));
            return line;
        }
        #endregion

        #region convert CustomerInLineDTO to customerInLine
        public static customersInLine GetCustomerInLine(CustomerInLineDTO appointment)
        {
            customersInLine customerInLine = new customersInLine()
            {
                ActualHour =appointment.ActualHour,
                activityTimeId=appointment.ActivityTimeId,
                statusTurn = appointment.StatusTurn ,
                custId = appointment.CustId ,
                preAlert = appointment.PreAlert ,
                TurnId = appointment.TurnId,
                estimatedHour=appointment.EstimatedHour,
                enterHour=appointment.EnterHour

            };
            return customerInLine;
        }

        public static List<customersInLine> GetCustomersInLine(List<CustomerInLineDTO> appointments)
        {
            List<customersInLine> line = new List<customersInLine>();
            appointments.ForEach(a => line.Add(GetCustomerInLine(a)));
            return line;
        }
        #endregion
    }
}
