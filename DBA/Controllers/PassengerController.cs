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


        /*[HttpPost]
        public IActionResult AddRoute(Route r)
        {
            var route = _context.Routes.Where(temp => temp.route_id == r.route_id).FirstOrDefault();
            route.no_of_passengers++;
            var route_id = route.route_id;
            _context.Routes.Update(route);
            _context.SaveChanges();
            return Ok(route_id);
        }
        [HttpPost]
        public IActionResult CheckOut(Route r)
        {
            var route = _context.Routes.Find(r.route_id);
            route.no_of_passengers--;
            _context.Routes.Update(route);
            _context.SaveChanges();
            return Ok();
        }*/

    }
}
