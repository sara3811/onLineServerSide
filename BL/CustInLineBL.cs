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
        public static List<DAL.customersInLine> GetTurnsToCustomer(int custId)
        {
            return CustInLineDal.GetTurnToCust(custId);
        }


    }
}
