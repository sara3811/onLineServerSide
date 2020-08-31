using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class CustomerDTO
    {
        public int CustId { get; set; }
        public string PhoneNumber { get; set; }
        public string CustName { get; set; }
        public int Latitude { get; set; }
        public int Longitude { get; set; }
        public int NumOfOrderTurns { get; set; }
        public int NumOfCanceledTurns { get; set; }
    }
}
