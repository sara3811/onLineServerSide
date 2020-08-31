using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ServiceDal
    {
        public static List<service> GetServicesByCategory(int categoryId)
        {
            try
            {
                using (onLineEntities1 entities1 = new onLineEntities1())
                {
                    return entities1.services.Include("business").Where(s => s.categoryId == categoryId).ToList();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static service GetServicById(int serviceId)
        {
            try
            {
                using (onLineEntities1 entities1 = new onLineEntities1())
                {
                    var a = entities1.services.Include("business").ToList();
                    var b=a.FirstOrDefault(s => s.serviceId == serviceId);
                    return b;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
