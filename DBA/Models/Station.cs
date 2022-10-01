using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DBA.Models
{
    public class Station
    {
        [Key]
        public long station_id { get; set; }
        public string location { get; set; }
    }
}
