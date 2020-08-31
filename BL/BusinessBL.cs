using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BL
{
    public class BusinessBL
    {
        public static List<BusinessDTO> GetBusinesses()
        {
            return converters.BusinessConverters.GetListBusinessDTO(BusinessDal.GetBusinesses());
        }
        public static BusinessDTO GetBusinessByPassword(string password)
        {
            try
            {
                return converters.BusinessConverters.GetBusinessDTO(BusinessDal.GetBusinessByPassword(password));

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
