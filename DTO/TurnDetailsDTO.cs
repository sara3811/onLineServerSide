using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TurnDetailsDTO
    {
        //from angular
        public int CustId { get; set; }
        public int TurnId { get; set; }
        public int PreAlert { get; set; }
        public int ServiceId { get; set; }
        public TimeSpan ActualHour { get; set; }
        public DateTime EstimatedHour { get; set; }
        public string VeriverificationCode { get; set; }
    }

}
