using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApplicationData.Context;
using ApplicationData.Data;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using ApplicationService.DTOs;
using X.PagedList;

namespace MotoShopMVC.Controllers
{
    public class MotorcyclesController : Controller
    {
        private List<MotorcycleDTO>? Motorcycles { get; set; }

        // GET: Studentsmvc
        public async Task<IActionResult> Index(string searchString, int? page)
        {
           

            if (!String.IsNullOrEmpty(searchString))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7243/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync($"api/Motorcycles/search/{searchString}");
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the Employee list
                        Motorcycles = JsonConvert.DeserializeObject<List<MotorcycleDTO>>(EmpResponse);
                    }
                }
                
            }
            else
            {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/Motorcycles");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    Motorcycles = JsonConvert.DeserializeObject<List<MotorcycleDTO>>(EmpResponse);
                }
            }

            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(Motorcycles.ToPagedList(pageNumber, pageSize));
        }
        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MotorcycleDTO? motorcycle = default;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"api/Motorcycles/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    motorcycle = JsonConvert.DeserializeObject<MotorcycleDTO>(EmpResponse);
                }
            }

            if (motorcycle == null)
            {
                return NotFound();
            }

            return View(motorcycle);
        }

        // GET: Studentsmvc/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Studentsmvc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MotorcycleDTO motorcycle)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                var json = JsonConvert.SerializeObject(motorcycle);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync("api/Motorcycles", content);
            }
            return RedirectToAction(nameof(Index));
        }


        // GET: Studentsmvc/Edit/5
        public async Task<IActionResult> EditGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            MotorcycleDTO? motorcycle = default;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"api/Motorcycles/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    motorcycle = JsonConvert.DeserializeObject<MotorcycleDTO>(EmpResponse);
                }
            }
            if (motorcycle == null)
            {
                return NotFound();
            }
            return View(motorcycle);
        }

        // POST: Studentsmvc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MotorcycleDTO motorcycle)
        {
            if (id != motorcycle.Id)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                var json = JsonConvert.SerializeObject(motorcycle);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PutAsync($"api/Motorcycles/{id}", content);
            }

            if (motorcycle == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Studentsmvc/Delete/5
        public async Task<IActionResult> DeleteGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            MotorcycleDTO? motorcycle = default;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"api/Motorcycles/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    motorcycle = JsonConvert.DeserializeObject<MotorcycleDTO>(EmpResponse);
                }
            }
            if (motorcycle == null)
            {
                return NotFound();
            }

            return View(motorcycle);
        }

        // POST: Studentsmvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                await client.DeleteAsync($"api/Motorcycles/{id}");
            }


            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {

            return Motorcycles.Any(e => e.Id == id);
        }
    }
}
