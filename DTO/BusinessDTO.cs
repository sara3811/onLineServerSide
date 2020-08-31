using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DTO
{
    public class BusinessDTO
    {
        public int BusinessId { get; set; }
        public string BusinessName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public int ChainManagerId { get; set; }
        public List<ServiceDTO> Services { get; set; }
    }
}
