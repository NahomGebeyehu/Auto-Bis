using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DBA.Models
{
    public class Bus
    {
        [Key]
        public string bus_id { get; set; }
        public bool is_active { get; set; }
        public long ? route_id { get; set; }
        public virtual Route Route{ get; set; }
}
}
