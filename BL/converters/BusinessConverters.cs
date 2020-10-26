using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;


namespace BL.converters
{
    public class BusinessConverters
    {

        public static business GetBusiness(BusinessDTO BusinessDto)
        {
            business business = new DAL.business()
            {
                businessName = BusinessDto.BusinessName ,
                password = BusinessDto.Password ,
                adress_street = BusinessDto.Address,

                managerid = BusinessDto.ChainManagerId
            };
            return business;
        }

        public static BusinessDTO GetBusinessDTO(business business)
        {
            BusinessDTO businessDTO = new BusinessDTO()
            {
                BusinessId = business.businessId ,
                BusinessName = business.businessName ,
                Password = business.password ,
                Address = business.adress_street + " " + business.adress_numOfStreet + " " + business.adress_city ,
                // ChainManagerId = (int) business.managerid ,
               Services = ServiceConverters.GetServicesDTO(business.services.ToList())
            };
            return businessDTO;
        }

        public static List<BusinessDTO> GetListBusinessDTO(List<business> lBusiness)
        {
            List<BusinessDTO> l = new List<BusinessDTO>();
            lBusiness.ForEach(b => l.Add(GetBusinessDTO(b)));
            return l;
        }

        public static List<business> GetListBusiness(List<BusinessDTO> lBusiness)
        {
            List<DAL.business> l = new List<business>();
            lBusiness.ForEach(b => l.Add(GetBusiness(b)));
            return l;
        }



        /// business to show

        public static TurnInBusinessDTO GetSmallBusinessDTO(business business)
        {
            TurnInBusinessDTO businessDTO = new TurnInBusinessDTO()
            {
                BusinessName = business.businessName ,
                Address = business.adress_city + " " + business.adress_street + " " + business.adress_numOfStreet ,
            };
            return businessDTO;
        }


        public static List<TurnInBusinessDTO> GetListSmallBusinessDTO(List<business> lBusiness)
        {
            List<TurnInBusinessDTO> l = new List<TurnInBusinessDTO>();
            lBusiness.ForEach(b => l.Add(GetSmallBusinessDTO(b)));
            return l;
        }

    }
}
