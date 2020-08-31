using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class UnusualDTO
    {
        public int UnusualId { get; set; }
        public int Average { get; set; }
        public int ActivityTimeId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public float StandardDeviation { get; set; }
    }
}
