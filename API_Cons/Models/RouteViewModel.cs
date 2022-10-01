using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Cons.Models
{
    public class RouteViewModel
    {
        public long route_id { get; set; }
        public string route_name { get; set; }
        public int no_of_passengers { get; set; }
        public int no_of_buses { get; set; }
    }
}
