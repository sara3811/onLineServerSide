using BL.converters;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CustInLineBL
    {
        public static List<DTO.TurnInBusinessDTO> GetTurnsToCustomer(int custId)
        {
            return TurnInBusinessConverters.GetTurnsToShowDTO(CustInLineDal.GetTurnToCust(custId));
        }


    }
}
