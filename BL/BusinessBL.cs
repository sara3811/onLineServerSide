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
        public static int AddBusiness(BusinessDTO businessToAdd)
        {
            int businessId = BusinessDal.AddBusiness(converters.BusinessConverters.GetBusiness(businessToAdd));
            return businessId;

        }
        /// <summary>
        /// get the avg per-day in all services of specific business
        /// </summary>
        /// <param name="businessId"></param>
        /// <returns></returns>
        public static List<List<double>> GetAvgForBusiness(int businessId)
        {
            //todo: check if it is cprrect
            List<List<double>> avgForService = new List<List<double>>();
            var services = DAL.BusinessDal.GetBusinessById(businessId).services;
            int cnt = 0;
            foreach (var item in services)
            {
                var activityTimes = ActivityTimeDal.GetActivityTimes(item.serviceId);
                for (int i = 0; i < 7; i++)
                {
                    avgForService[cnt][i] = activityTimes.Where(a => a.dayInWeek == i + 1).Average(a => a.avgWaitings.Value);
                }
                cnt++;
            }
            return avgForService;
        }

    }
}
