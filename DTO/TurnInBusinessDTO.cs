using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TurnInBusinessDTO
    {
        public int ServiceId { get; set; }
        public string BusinessName { get; set; }
        public string Address { get; set; }
        //todo:לשנות שיחזיר dateTime
        public TimeSpan? EstimatedHour { get; set; }
        public int TurnId { get; set; }
        public int Duration { get; set; }

    }
}
