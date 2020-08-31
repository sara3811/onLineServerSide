using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CustomerDal
    {
        public static void AddCustomer(customer customer)
        {
            try
            {
                using (onLineEntities1 entities1 = new onLineEntities1())
                {
                    entities1.customers.Add(customer);
                    entities1.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static int GetCustId(string phone)
        {
            try
            {
                using (onLineEntities1 entities1 = new onLineEntities1())
                {
                    return  entities1.customers.FirstOrDefault(c => c.phoneNumber == phone).custId;
                }
            }
            catch(NullReferenceException)
            {
                throw new NullReferenceException("משתמש לא קיים");
    }
            catch
            {
                throw;
            }
        }
    }
}
