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
               return   entities1.customersInLines.Include(a => a.activityTime).Include(a => a.activityTime.service).Include("activityTime.service.business").Where(t => t.custId == custId &&( t.ActualHour == new TimeSpan()||t.ActualHour==null)).ToList();
               
            }
            //x => x.Quotes.Select(q => q.QuoteItems)
        }
    }
}
