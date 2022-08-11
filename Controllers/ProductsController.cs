using Microsoft.AspNetCore.Mvc;
using Rocket_Elevator_Customer_Portal.Models;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;


namespace Rocket_Elevator_Customer_Portal.Controllers
{
    public class ProductsController : Controller
    {
        public class DataObject
    {
        public long Id { get; set; }
        public string? Email_Of_The_Company_Contact { get; set; }
        public List<Building>? Buildings { get; set; }
        }

        public class Building
        {
            public long Id { get; set; }
            public string? Full_Name_Of_The_Building_Administrator { get; set; }
            //public string? Email_Of_The_Administrator_Of_The_Building { get; set; }
            //public string? Phone_Number_Of_The_Building_Administrator { get; set; }
            //public string? Full_Name_Of_The_Technical_Contact_For_The_Building { get; set; }
            //public string? Technical_Contact_Email_For_The_Building { get; set; }
            //public string? Technical_Contact_Phone_For_The_Building { get; set; }
            //public long customer_id { get; set; }
            //public long address_id { get; set; }
            public List<Battery>? batteries { get; set; }
            
        }
        public class Battery
        {
            public long Id { get; set; }
            public string? Status { get; set; }
            public string? Certificate_Of_Operations { get; set; }
            public string? Notes { get; set; }
            public DateTime? Date_Of_Commissioning { get; set; }
            public List<Column>? columns { get; set; }
        }
        public class Column
        {
            public long Id { get; set; }
            public string? Status { get; set; }
            public string? Notes { get; set; }
            public List<Elevator>? elevators { get; set; }
        }
        public class Elevator
        {
            public long Id { get; set; }
            public string? Status { get; set; }
            public string? Model { get; set; }
            public string? Notes { get; set; }
            public DateTime? Date_Of_Last_Inspection { get; set; }
        }

        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;
        }

        public IActionResult Intervention()
        {
            return View();
        }

        public IActionResult Index()
        {
            const string URL = "https://rest-api-francis-renauld.herokuapp.com/api/email/";
            using (HttpClient client = new HttpClient())
            {

                var email = User.Identity.Name;
                client.BaseAddress = new Uri(URL);
                client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync(email).Result;
                var fullUrl = URL + email;
                if (response.IsSuccessStatusCode)
                {
                    var customer = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;
                    ViewBag.customer = customer;
                    return View();
                }
                    else
                    {
                        return NotFound("Not found");
                    }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}