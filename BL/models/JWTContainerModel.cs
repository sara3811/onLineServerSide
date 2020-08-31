using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace BL.models
{
    public class JWTContainerModel : IAuthContainerModel
    {
        public string SecretKey { get; set; } = "gfhWE545hYFV543VBNBhbv34";
        public string SecurityAlgorithm { get; set; } = SecurityAlgorithms.HmacSha256Signature;
        public int ExpireMinutes { get; set; } = 10080;
        public Claim[] Claims { get; set ; }
    }
}
