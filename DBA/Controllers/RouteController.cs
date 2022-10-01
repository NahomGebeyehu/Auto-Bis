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
    public class RouteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public RouteController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Routes()
        {
            var rw = _context.Routes.ToList();
            return Ok(rw);
        }

        [HttpGet("{route_id}")]
        public IActionResult GetRouteById(long route_id)
        {
            var route = _context.Routes.Find(route_id);
            return Ok(route);
        }

        [Route("AddRoute")]
        [HttpPut]
        public IActionResult AddRoute(Route r)
        {
            var route = _context.Routes.Where(temp => temp.route_id == r.route_id).FirstOrDefault();
            route.no_of_passengers++;
            var route_id = route.route_id;
            _context.Routes.Update(route);
            _context.SaveChanges();
            return Ok(route_id);
        }

        [Route("CheckOut")]
        [HttpPut]
        public IActionResult CheckOut(Route r)
        {
            var route = _context.Routes.Find(r.route_id);
            route.no_of_passengers--;
            _context.Routes.Update(route);
            _context.SaveChanges();
            return Ok();
        }

        
        
    }
}
