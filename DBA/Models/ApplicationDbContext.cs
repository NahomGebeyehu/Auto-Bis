using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBA.Models
{
    
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
               : base(options)
            {
            }

            public DbSet<Bus> Buses { get; set;}
            public DbSet<Passenger> Passengers { get; set; }
            public DbSet<Route> Routes { get; set; }
    }
    }
