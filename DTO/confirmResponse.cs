using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
   public class confirmResponse
    {
        public string verificationCode { get; set; }
        public bool isConflict { get; set; }
        public int turnId { get; set; }
    }
}
