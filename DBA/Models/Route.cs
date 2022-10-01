using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DBA.Models
{
    public class Route
    {
        [Key]
        public long route_id { get; set; }
        public string route_name {get; set;}
        public int no_of_passengers { get; set; }
        public int no_of_buses { get; set; }
    }
}
