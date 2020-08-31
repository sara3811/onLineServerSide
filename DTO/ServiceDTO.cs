using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ServiceDTO
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int BusinessId { get; set; }
        public int CategoryId { get; set; }
        public bool KindOfPermission { get; set; }
    }
}
