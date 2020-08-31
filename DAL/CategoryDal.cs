using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CategoryDal
    {
        public static List<category> GetCategories()
        {
            try
            {
                using (onLineEntities1 entities = new onLineEntities1())
                {
                    return entities.categories.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
