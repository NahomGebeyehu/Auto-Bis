using DBA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBA.Controllers
{
    public class SystemAdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SystemAdminController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult bus_page()
        {

            var bt = _context.Buses.ToList();
            ViewBag.total_Bus = bt.Count();
            return View(bt);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddBus()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddBus(Bus b)
        {
            _context.Buses.Add(b);
            _context.SaveChanges();
            return RedirectToAction("bus_page");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteBus(string bus_id)
        {
            var bs = _context.Buses.Find(bus_id);
            _context.Buses.Remove(bs);
            _context.SaveChanges();
            return RedirectToAction("bus_page");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult route_page()
        {
            var rt = _context.Routes.ToList();
            return View(rt);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddRoute()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddRoute(Route r)
        {
            _context.Routes.Add(r);
            _context.SaveChanges();
            return RedirectToAction("route_page");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteRoute(Route r)
        {
            var rt = _context.Routes.Find(r.route_id);
            _context.Routes.Remove(rt);
            var bs = _context.Buses.Where(temp => temp.route_id == r.route_id).ToList();
            if (bs.Count != 0)
            {
                for (int i = bs.Count() - 1; i >= 0; i--)
                {
                    bs[i].is_active = false;
                }
                _context.Buses.UpdateRange(bs);
            }

            _context.SaveChanges();
            return RedirectToAction("route_page");
        }
    }
}
