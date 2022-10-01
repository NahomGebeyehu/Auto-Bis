using DBA.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBA.Controllers
{
    public class VAMController : Controller
    {
        private readonly ApplicationDbContext _context;
        public VAMController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "VAM")]
        public IActionResult SignIn()
        {
            return View();
        }
        [Authorize(Roles = "VAM")]
        [HttpPost]
        public IActionResult SignIn(VAM V)
        {
            var rw = _context.VAMs.Where(temp => temp.vam_id ==V.vam_id  && temp.password == V.password).FirstOrDefault();
            if (rw != null)
                return RedirectToAction("Index");
            else
            {

                return RedirectToAction("SignIn");
            }
        }
        [Authorize(Roles = "VAM")]
        public IActionResult Index()
        {
            
            var rt = _context.Routes.ToList();
            rt = rt.OrderByDescending(x =>(x.no_of_passengers+1)/(x.no_of_buses+1)).ToList();
            ViewBag.avail_Bus=CountBusAvailable();
                return View(rt);
        }
        [Authorize(Roles = "VAM")]
        [HttpPost]
        public IActionResult Allocate(Route r, int no_of_buses)
        {
            
            //  int recommended = r.no_of_passengers / 10;
            var rt = _context.Routes.Find(r.route_id);
            var av_bus = _context.Buses.Where(temp => temp.is_active == false).ToList();
            if(CountBusAvailable()!=0)
            {
                for (int i = no_of_buses - 1; i >= 0; i--)
                {
                    av_bus[i].route_id = r.route_id;
                    av_bus[i].is_active = true;

                }
                rt.no_of_buses = rt.no_of_buses + no_of_buses;
            }
            else
            {
                return NotFound();
            }

            _context.Update(rt);
            _context.UpdateRange(av_bus);
             _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "VAM")]
        public IActionResult Dealloacte()
        {
            var rt = _context.Routes.ToList();
            rt = rt.OrderByDescending(x => (x.no_of_buses)- (x.no_of_passengers/10)).ToList();
            ViewBag.avail_Bus = CountBusAvailable();
            return View(rt);
        }
        [Authorize(Roles = "VAM")]
        [HttpPost]
        public IActionResult Dealloacte(Route r,int no_of_buses)
        {
            var av_bus = _context.Buses.Where(temp => temp.route_id == r.route_id).ToList();
            var rt = _context.Routes.Find(r.route_id);
            if (no_of_buses<=r.no_of_buses)
            {
                if (av_bus.Count() != 0)
                {
                    for (int i = no_of_buses - 1; i >= 0; i--)
                    {
                        av_bus[i].is_active = false;

                    }
                    rt.no_of_buses = rt.no_of_buses - no_of_buses;
                    _context.Update(rt);
                    _context.UpdateRange(av_bus);
                    _context.SaveChanges();
                  
                }
                return RedirectToAction("Index");
            }
            else
                return NotFound();
            
        }
        [Authorize(Roles = "VAM")]
        public int CountBusAvailable()  
        {
            var bs= _context.Buses.Where(temp => temp.is_active == false);
            return bs.Count();
        }

        public int recommendedDeallocate(int bus_no,int passenger_no)
        {
            return (bus_no-(passenger_no/10));
        }
    }
}

