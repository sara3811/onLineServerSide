using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
public class ServiceBL
    {
        public static double GetAvgDurationByService(List<DAL.activityTime>activityTimes)
        {
            
            return activityTimes.Average(a => a.avgServiceDuration.Value);
        }

        public static double GetAvgForAllServicesPerCategory(int categoryId)
        {
            double avg = 0;
            var services = DAL.ServiceDal.GetServicesByCategory(categoryId);
            foreach (var item in services)
            {
                avg += GetAvgDurationByService(item.activityTimes.ToList());
            }

         return avg /= services.Count();
        }

        public static List<DTO.ServiceDTO> GetServicesInformation(int bussinessId)
        {
            var services = DAL.ServiceDal.GetServicesByBussiness(bussinessId);
            return converters.ServiceConverters.GetServicesDTO(services);
        }
    }
}
