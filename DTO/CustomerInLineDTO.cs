using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CustomerInLineDTO
    {
       
        public int TurnId { get; set; }
        public int CustId { get; set; }
        public int ActivityTimeId { get; set; }
        public TimeSpan EnterHour { get; set; }
        public DateTime EstimatedHour { get; set; }
        public TimeSpan ActualHour { get; set; }
        public int PreAlert { get; set; }
        public int StatusTurn { get; set; }


    }
}
