using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UnusualDal
    {
        public static List<unusual> GetUnusuals(int serviceId)
        {
            using (onLineEntities1 entities1 = new onLineEntities1())

            {
                return entities1.unusuals.Where(u => u.activityTime.serviceId == serviceId).ToList();
            }
        }

        public static void AddUnUsual(unusual unusual)
        {
            try
            {
                using (onLineEntities1 entities1 = new onLineEntities1())
                {
                    entities1.unusuals.Add(unusual);
                    entities1.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
