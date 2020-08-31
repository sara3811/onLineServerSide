using BL.manager;
using BL.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BL
{
    public class Token
    {
        private static JWTContainerModel getJWTContainerModel(string name, string phone)
        {
            return new JWTContainerModel()
            {
                Claims = new Claim[]
                {
                    new Claim(ClaimTypes.Name,name),
                    new Claim(ClaimTypes.MobilePhone,phone),
                }
            };
        }

        public static string GetToken(string name, string phone)
        {
            IAuthContainerModel model = getJWTContainerModel(name, phone);
            IAuthService authService = new JWTService(model.SecretKey);

            string token = authService.GenerateToken(model);

            return token;
        }

        private static string getPhoneFromToken(string token)
        {
            try
            {
                string tokenPhone;
                IAuthContainerModel model = new JWTContainerModel();

                IAuthService authService = new JWTService(model.SecretKey);
                List<Claim> claims = authService.GetTokenClaims(token).ToList();

                string tokenName = claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.Name)).Value;
                tokenPhone = claims.FirstOrDefault(e => e.Type.Equals(ClaimTypes.MobilePhone)).Value;

                return tokenPhone;
            }
            catch
            {
                throw;
            }
        }

        public static int GetCustIdFromToken(string token)
        {
            try
            {
                string phone = getPhoneFromToken(token);
                return CustomerDal.GetCustId(phone);
            }
            catch (NullReferenceException ex)
            {
                throw ex;
            }
            catch
            {
                throw;
            }
        }

        public static void AddCustomer(string name, string phone)
        {
            customer customer = new customer() { custName = name, phoneNumber = phone };
            CustomerDal.AddCustomer(customer);
        }
    }
}
