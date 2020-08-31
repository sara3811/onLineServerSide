using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CategoryBL
    {
        public static List<DTO.CategoryDTO> GetCategories()
        {

            //toask: timer
            try
            {
                return converters.CategoryConverters.GetCategoriesDTO(DAL.CategoryDal.GetCategories());
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
