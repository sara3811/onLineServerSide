using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.services
{
public   class onLineInformation
    {
        public static List<int> GetGeneralInformation()
        {
            List<int> information = new List<int>();
            information.Add(DAL.CustInLineDal.GetNumOfTurns());
            information.Add(DAL.BusinessDal.GetNumOfBusinesses());
            information.Add(DAL.CategoryDal.GetNumOfCategories());
            information.Add(DAL.CustomerDal.GetNumOfCustomers());


            return information;

        }
    }
}
