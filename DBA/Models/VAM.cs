﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DBA.Models
{
    public class VAM
    {
        [Key]
        public long vam_id { get; set;}
        public string password { get; set; }
    }
}
