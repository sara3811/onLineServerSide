using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace BL.converters
{
    class CategoryConverters
    {

        private static CategoryDTO getCategoryDTO(DAL.category category)
        {

            CategoryDTO categoryDTO = new CategoryDTO()
            {
                CategoryId = category.categoryId,
                CategoryName = category.categoryName,
            };
            return categoryDTO;
        }
        public static List<CategoryDTO> GetCategoriesDTO(List<DAL.category> categories)
        {

            List<CategoryDTO> l = new List<CategoryDTO>();
            categories.ForEach(c => l.Add(getCategoryDTO(c)));
            return l;
        }

    }
}
