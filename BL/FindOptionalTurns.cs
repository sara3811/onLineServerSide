using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using DTO;
using BL.services;
namespace BL
{
    public class FindOptionalTurns
    {
        /// <summary>
        /// הפונקציה יוצרת רשימת עסקים עם שעות אפשריות ומרחק של כל עסק מהמשתמש 
        /// הפונקציה מסננת את הרשימה 
        /// </summary>
        /// <param name="categoryId">קטגוריה</param>
        /// <param name="latitude">קו אורך</param>
        /// <param name="longitude">קו רוחב</param>
        /// <param name="isDriving">האם מגיע ברכב או ברגל</param>
        /// <returns>רשימת עסקים מסוננת עם שעה אפשרית</returns>
        public static List<TurnInBusinessDTO> GetPossibleBusinessesWithHour(int categoryId, string latitude, string longitude, bool isDriving, int custId)
        {
            List<TurnInBusinessDTO> services = new List<TurnInBusinessDTO>();
            List<TurnInBusinessDTO> servicesToReturn = new List<TurnInBusinessDTO>();
            bool pushFlag = false;
            services = converters.TurnInBusinessConverters.GetTurnsInBusinessDTO(ServiceDal.GetServicesByCategory(categoryId));
            services.ForEach(s => s.Duration = TurnServices.GooglePlaces(longitude, latitude, s.Address, isDriving));
            if (services.Count() > 20)
                services.OrderBy(s => s.Duration).Take(20);
            TimeSpan timeToLookFor;
            services.ForEach(s => s.EstimatedHour = ImmediateTurn.GetOptionalHourPerBusiness(s.ServiceId, timeToLookFor = TimeSpan.FromMinutes(s.Duration).Add(DateTime.Now.TimeOfDay), ref pushFlag));
            services.OrderBy(s => s.EstimatedHour);

            services.RemoveAll(s => s.EstimatedHour == new TimeSpan());
            servicesToReturn.AddRange(services.Take(2));
            services.RemoveAll(s => servicesToReturn.Contains(s));
            servicesToReturn.AddRange(services.Where(s => s.Duration == services.Min(d => d.Duration)));

            if (servicesToReturn.Count == 0)
                return servicesToReturn;
            servicesToReturn.ForEach(s => s.TurnId = ImmediateTurn.MakeTemporaryTurn(s, pushFlag, custId));


            //todoever: להחזיר אוביקטים  לפי הסטוריה מועדפים וכו
            return servicesToReturn;
        }


        public static TurnInBusinessDTO GetPossibleBusinessWithHour(int serviceId, string latitude, string longitude, bool isDriving, int custId)
        {
            bool pushFlag = false;
            TurnInBusinessDTO service = new TurnInBusinessDTO();
            service = converters.TurnInBusinessConverters.GetTurnInBusinessDTO(ServiceDal.GetServicById(serviceId));
            service.Duration = TurnServices.GooglePlaces(longitude, latitude, service.Address, isDriving);
            service.EstimatedHour = ImmediateTurn.GetOptionalHourPerBusiness(serviceId, TimeSpan.FromMinutes(service.Duration).Add(DateTime.Now.TimeOfDay), ref pushFlag);

            try
            {
                if(service.EstimatedHour!=new TimeSpan())
                service.TurnId = ImmediateTurn.MakeTemporaryTurn(service, pushFlag, custId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return service;
        }
    }
}
