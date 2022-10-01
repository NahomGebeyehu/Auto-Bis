using System;
using API_Cons.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
namespace API_Consume.Controllers
{
    public class PassengerController : Controller
    {
        // GET: Passenger
        Uri baseAddress = new Uri("https://localhost:44369/api");
        HttpClient client;
        public PassengerController()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
        }

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(PassengerViewModel p)
        {
            PassengerViewModel model = new PassengerViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + string.Format("/Passenger/{0}/{1}", p.username, p.password)).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<PassengerViewModel>(data);
                if (model != null)
                    //return RedirectToAction("CheckedIn");
                    return RedirectToAction("UserPage",model);
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult SignUp()
        {

            return View();
        }
        [HttpPost]
        public ActionResult SignUp(PassengerViewModel p)
        {
            if (!ModelState.IsValid)
            {
                return View("Index");
            }

            String data = JsonConvert.SerializeObject(p);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/Passenger/PostPassenger", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        public IActionResult CheckedIn()
        {
            List<RouteViewModel> modelList = new List<RouteViewModel>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/Route").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<List<RouteViewModel>>(data);
                return View(modelList);
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult CheckedIn(RouteViewModel r)
        {  
                RouteViewModel model = new RouteViewModel();
            HttpResponseMessage rsponse = client.GetAsync(client.BaseAddress + string.Format("/Route/{0}", r.route_id)).Result;
                if (rsponse.IsSuccessStatusCode)
                {
                    string dt = rsponse.Content.ReadAsStringAsync().Result;
                    model = JsonConvert.DeserializeObject<RouteViewModel>(dt);
                }
            else
            {
                return NotFound();
            }

                String data = JsonConvert.SerializeObject(r);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Route/AddRoute", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return View("CheckOut", model);
            }
            else
            {
                return NotFound();
            }
            
        }
        [HttpPost]
        public IActionResult CheckOut(RouteViewModel r)
        {

            String data = JsonConvert.SerializeObject(r);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Route/CheckOut", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        public IActionResult UserPage(PassengerViewModel p)
        {
            return View(p);
        }
    [HttpPost]
        public IActionResult Manage(string username, string password)
        {
            PassengerViewModel model = new PassengerViewModel();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + string.Format("/Passenger/{0}/{1}", username,password)).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<PassengerViewModel>(data);
                return View(model);
            }
            else
                return NotFound();
        }

        [HttpPost]
        public IActionResult Update(PassengerViewModel r)
        {

            String data = JsonConvert.SerializeObject(r);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/Passenger", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult Delete(PassengerViewModel r)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + string.Format("/Passenger/{0}", r.username)).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return NotFound();
        }
    }
}