using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BusinessDal
    {
        public static List<business> GetBusinesses()
        {
            try
            {
                using (onLineEntities1 entities1 = new onLineEntities1())
                {
                    return entities1.businesses.Include("services").ToList();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static business GetBusinessById(int id)
        {
            try
            {
                using (onLineEntities1 entities1 = new onLineEntities1())
                {
                    return entities1.businesses.Include("services").FirstOrDefault(b => b.businessId == id);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static business GetBusinessByPassword(string password)
        {
            try
            {
                using (onLineEntities1 entities1 = new onLineEntities1())
                {
                    business business = entities1.businesses.Include("services").FirstOrDefault(b => b.password == password);
                    if (business == null)
                        throw new Exception("worng password");
                    return business; 
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int AddBusiness(business business)
        {
            using (onLineEntities1 entities1 = new onLineEntities1())
            {
                entities1.businesses.Add(business);
                entities1.SaveChanges();
                int id = business.businessId;
                return id;
            }
        }

        public static int GetNumOfBusinesses()
        {
            using (onLineEntities1 entities1 = new onLineEntities1())
            {
               return entities1.businesses.Count();
            }
        }

    }
}
