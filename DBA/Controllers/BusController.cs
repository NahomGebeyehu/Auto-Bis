using DBA.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBA.Controllers
{
    public class BusController : Controller
    {
        private readonly ApplicationDbContext _context;
    public BusController(ApplicationDbContext context)
    {
        _context = context;
    }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult DriveTo(string bus_id)
        {
            var bus = _context.Buses.Find(bus_id);
            return View(bus);
        }
    }
}
