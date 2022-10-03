using DBA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public PassengerController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("{username}/{password}")]
        public IActionResult GetUser(string username,string password)
        {
            var usr = _context.Passengers.Find(username);
            if (usr.password == password)
                return Ok(usr);
            else return NotFound();
        }
        [Route("PostPassenger")]
        [HttpPost]
        public IActionResult PostPassenger(Passenger p)
        {
            _context.Passengers.Add(p);
            _context.SaveChanges();
            return Ok(p);
        }

        [HttpPut]
        public IActionResult PutPassenger(Passenger p)
        {
            _context.Passengers.Update(p);
            _context.SaveChanges();
            return Ok(p);
        }

        [HttpDelete("{username}")]
        public IActionResult DeletePassenger(string username)
        {
            var p = _context.Passengers.Find(username);
            _context.Passengers.Remove(p);
            _context.SaveChanges();
            return Ok(p);
        }
    }
}
