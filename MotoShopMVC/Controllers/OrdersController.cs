using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ApplicationData.Context;
using ApplicationData.Data;
using ApplicationService.DTOs;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using X.PagedList;

namespace MotoShopMVC.Controllers
{
    public class OrdersController : Controller
    {
        private List<OrderDTO>? Orders { get; set; }
        // GET: Studentsmvc
        public async Task<IActionResult> Index(string searchString, int? page)
        {


            if (!String.IsNullOrEmpty(searchString))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7243/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync($"api/Orders/search/{searchString}");
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the Employee list
                        Orders = JsonConvert.DeserializeObject<List<OrderDTO>>(EmpResponse);
                    }
                }

            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7243/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync("api/Orders");
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the Employee list
                        Orders = JsonConvert.DeserializeObject<List<OrderDTO>>(EmpResponse);
                        
                    }
                }

            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(Orders.ToPagedList(pageNumber, pageSize));
        }
        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            OrderDTO? order = default;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"api/Orders/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    order = JsonConvert.DeserializeObject<OrderDTO>(EmpResponse);
                }
            }

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Studentsmvc/Create
        public async Task<IActionResult> Create()
        {
            List<MotorcycleDTO> motorcycles = new List<MotorcycleDTO>();
            List<UserDTO> users = new List<UserDTO>();
                    List<MotorcycleDTO?> available = new List<MotorcycleDTO?>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"api/Motorcycles");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    motorcycles = JsonConvert.DeserializeObject<List<MotorcycleDTO>>(EmpResponse);
                    foreach(var item in motorcycles)
                    {
                        if (item.IsAvailable)
                        {
                            available.Add(item);
                        }
                    }
                    
                }
                Res = await client.GetAsync("api/Users");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    users = JsonConvert.DeserializeObject<List<UserDTO>>(EmpResponse);
                }
            }

                    ViewBag.MotorcycleId = new SelectList(available, "Id", "Make");
                    ViewBag.UserId = new SelectList(users, "Id", "FName");

            return View();
        }

        // POST: Studentsmvc/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderDTO order)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                var json = JsonConvert.SerializeObject(order);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync("api/Orders", content);
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

            List<MotorcycleDTO> motorcycles = new List<MotorcycleDTO>();
            List<UserDTO> users = new List<UserDTO>();
            List<MotorcycleDTO?> available = new List<MotorcycleDTO?>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"api/Motorcycles");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    motorcycles = JsonConvert.DeserializeObject<List<MotorcycleDTO>>(EmpResponse);
                    foreach (var item in motorcycles)
                    {
                        if (item.IsAvailable)
                        {
                            available.Add(item);
                        }
                    }

                }
                Res = await client.GetAsync("api/Users");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    users = JsonConvert.DeserializeObject<List<UserDTO>>(EmpResponse);
                }
            }

            OrderDTO? order = default;
           
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"api/Orders");
                if (Res.IsSuccessStatusCode)
                {
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    Orders = JsonConvert.DeserializeObject<List<OrderDTO>>(EmpResponse);
                    //Storing the response details recieved from web api
                    Res = await client.GetAsync($"api/Orders/{id}");
                    var response = Res.Content.ReadAsStringAsync().Result;
                    order = JsonConvert.DeserializeObject<OrderDTO>(response);
                    
                   
                }
            }
            ViewBag.MotorcycleId = new SelectList(available, "Id", "Make");
            ViewBag.UserId = new SelectList(users, "Id", "FName");

            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Studentsmvc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OrderDTO order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                var json = JsonConvert.SerializeObject(order);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PutAsync($"api/Orders/{id}", content);
            }

            if (order == null)
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

            OrderDTO? order = default;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"api/Orders/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    order = JsonConvert.DeserializeObject<OrderDTO>(EmpResponse);
                }
            }
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Studentsmvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                await client.DeleteAsync($"api/Orders/{id}");
            }


            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {

            return Orders.Any(e => e.Id == id);
        }
    }
}
