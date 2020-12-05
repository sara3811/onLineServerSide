using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;

namespace BL.converters
{
    class ServiceConverters
    {
        public static service GetService(ServiceDTO serviceDTO)
        {
           service service = new service()
            {
                serviceId = serviceDTO.ServiceId ,
                serviceName = serviceDTO.ServiceName ,               
                categoryId = serviceDTO.CategoryId ,
                kindOfPermission=serviceDTO.KindOfPermission,
            };
            return service;
        }

        public static ServiceDTO GetServiceDTO(service service)
        {
            ServiceDTO serviceDTO = new ServiceDTO()
            {
                ServiceId = service.serviceId,
                ServiceName = service.serviceName,
                AvgNumOfWorkers =(int) service.activityTimes.Average(a => a.numOfWorkers),
                AvgServiceDuration = ServiceBL.GetAvgDurationByService(service.activityTimes.ToList()),
                AvgSameServices= ServiceBL.GetAvgForAllServicesPerCategory(service.categoryId),
                CategoryId = service.categoryId ,
                KindOfPermission=service.kindOfPermission,
            };
            return serviceDTO;
        }

        public static List<ServiceDTO> GetServicesDTO(List<service> services)
        {
            List<ServiceDTO> l = new List<ServiceDTO>();
            services.ForEach(s => l.Add(GetServiceDTO(s)));
            return l;
        }
    }
}
