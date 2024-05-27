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
    public class UsersController : Controller
    {
        private List<UserDTO>? Users { get; set; }
        // GET: Studentsmvc
        public async Task<IActionResult> Index(string searchString, int? page)
        {


            if (!String.IsNullOrEmpty(searchString))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7243/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync($"api/Users/search/{searchString}");
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the Employee list
                        Users = JsonConvert.DeserializeObject<List<UserDTO>>(EmpResponse);
                    }
                }

            }
            else
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7243/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = await client.GetAsync("api/Users");
                    if (Res.IsSuccessStatusCode)
                    {
                        //Storing the response details recieved from web api
                        var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                        //Deserializing the response recieved from web api and storing into the Employee list
                        Users = JsonConvert.DeserializeObject<List<UserDTO>>(EmpResponse);
                    }
                }

            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(Users.ToPagedList(pageNumber, pageSize));
        }
        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            UserDTO? user = default;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"api/Users/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    user = JsonConvert.DeserializeObject<UserDTO>(EmpResponse);
                }
            }

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
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
        public async Task<IActionResult> Create(UserDTO motorcycle)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                var json = JsonConvert.SerializeObject(motorcycle);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PostAsync("api/Users", content);
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
            UserDTO? user = default;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"api/Users/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    user = JsonConvert.DeserializeObject<UserDTO>(EmpResponse);
                }
            }
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Studentsmvc/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UserDTO user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                var json = JsonConvert.SerializeObject(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                await client.PutAsync($"api/Users/{id}", content);
            }

            if (user == null)
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

            UserDTO? user = default;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync($"api/Users/{id}");
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var EmpResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    user = JsonConvert.DeserializeObject<UserDTO>(EmpResponse);
                }
            }
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Studentsmvc/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7243/");
                await client.DeleteAsync($"api/Users/{id}");
            }


            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {

            return Users.Any(e => e.Id == id);
        }
    }
}
