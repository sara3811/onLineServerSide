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
            if (services == null)
                throw new Exception("there is no services!");
            int cnt = 0;
            foreach (var item in services)
            {
                var activityTimes = ActivityTimeDal.GetActivityTimes(item.serviceId);
                if (activityTimes == null)
                    throw new Exception("there is no activityTimes!");
               
                List<double> days = new List<double>();
                     
                for (int i = 0; i < 7; i++)
                {
                    var dailyAC = activityTimes.Where(a => a.dayInWeek == i + 1);
                    if (dailyAC != null && dailyAC.Count() > 0)
                        days.Add(dailyAC.Average(a => a.avgWaitings.Value));
                    else
                        days.Add(0);
                }
                avgForService.Add(days);
                cnt++;
            }
            return avgForService;
        }
        //todo: finish with it
        public static List<int> GetGeneralInformationForBusiness(int businessId)
        {
            List<int> information = new List<int>();
            information.AddRange(services.onLineInformation.GetGeneralInformation());
           // information.Add


            return information;

        }


    }
}
