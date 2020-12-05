using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAL;

namespace BL.converters
{
    class BasicServiceConverters
    {
        public static service GetService(BasicServiceDTO serviceDTO)
        {
            service service = new service()
            {
                serviceId = serviceDTO.ServiceId,
                serviceName = serviceDTO.ServiceName,
                businessId = serviceDTO.BusinessId,
                categoryId = serviceDTO.CategoryId,
                kindOfPermission = serviceDTO.KindOfPermission,
            };
            return service;
        }

        public static BasicServiceDTO GetBasicServiceDTO(service service)
        {
            BasicServiceDTO serviceDTO = new BasicServiceDTO()
            {
                ServiceId = service.serviceId,
                ServiceName = service.serviceName,
                BusinessId = service.businessId,
                CategoryId = service.categoryId,
                KindOfPermission = service.kindOfPermission,
            };
            return serviceDTO;
        }

        public static List<BasicServiceDTO> GetBasicServicesDTO(List<service> services)
        {
            List<BasicServiceDTO> l = new List<BasicServiceDTO>();
            services.ForEach(s => l.Add(GetBasicServiceDTO(s)));
            return l;
        }
    }

}
