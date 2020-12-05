using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace DAL
{
    public class CustInLineDal
    {
        public static List<customersInLine> GetTurnToCust(int custId)
        {
            using (onLineEntities1 entities1 = new onLineEntities1())

            {
               return   entities1.customersInLines.Include(a => a.activityTime).Include(a => a.activityTime.service).Include("activityTime.service.business").Where(t => t.custId == custId &&( t.actualHour == new TimeSpan()||t.actualHour==null)).ToList();
               
            }
            //x => x.Quotes.Select(q => q.QuoteItems)
        }

        public static int GetNumOfTurnsByBusiness(int businessId)
        {
            using (onLineEntities1 entities1 = new onLineEntities1())
            {
                return entities1.customersInLines.Include(a => a.activityTime).Include(a => a.activityTime.service).Where(c=>c.activityTime.service.businessId==businessId).Count();
            }
        }

        public static int GetNumOfTurns()
        {
            using (onLineEntities1 entities1 = new onLineEntities1())
            {
                return entities1.customersInLines.Count();
            }
        }
    }
}
