using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DBA.Models
{
    public class Passenger
    {
        [Key]
        public string username { get; set; }
        public string password { get; set; }
        public long phone_no { get; set; }
    }
}
